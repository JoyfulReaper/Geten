﻿using Geten.Core;
using System.Collections.Generic;
using System.IO;

namespace Geten.Runtime.Binary
{
	public class BinaryGameDefinitionFile
	{
		public BinaryGameFileHeader Header { get; set; } = new BinaryGameFileHeader();
		public List<BinaryGameSection> Sections { get; set; } = new List<BinaryGameSection>();

		public BinaryGameSection this[CaseSensisitiveString name]
		{
			get
			{
				foreach (var s in Sections)
				{
					if (s.Header.Name == name)
					{
						return s;
					}
				}

				throw new System.Exception($"No Section called '{name}' found");
			}
		}

		public static BinaryGameDefinitionFile Load(Stream strm)
		{
			var br = new BinaryReader(strm);
			var result = new BinaryGameDefinitionFile();
			result.Header = ReadHeader(br);
			result.Sections = ReadSections(br, result.Header.SectionCount);

			br.Close();

			return result;
		}

		public byte[] GetBodyOfSection(CaseSensisitiveString name)
		{
			foreach (var s in Sections)
			{
				if (s.Header.Name == name)
				{
					return s.Body;
				}
			}

			throw new System.Exception($"No Section called '{name}' found");
		}

		public void Save(Stream strm)
		{
			var bw = new BinaryWriter(strm);
			bw.Write(0xC0FFEE); // Magic Number
			bw.Write(Header.SectionCount);

			WriteSections(bw);

			bw.Close();
		}

		private static BinaryGameFileHeader ReadHeader(BinaryReader br)
		{
			var header = new BinaryGameFileHeader();
			header.MagicNumber = br.ReadInt32();

			if (header.MagicNumber != 0xC0FFEE)
			{
				throw new InvalidDataException("Invalid File Format for Binary Game Definition");
			}
			else
			{
				header.SectionCount = br.ReadByte();
			}

			return header;
		}

		private static BinaryGameSection ReadSection(BinaryReader br)
		{
			var s = new BinaryGameSection();

			s.Header = ReadSectionHeader(br);
			s.Body = br.ReadBytes(s.Header.SectionLength);

			return s;
		}

		private static BinaryGameSectionHeader ReadSectionHeader(BinaryReader br)
		{
			var header = new BinaryGameSectionHeader();

			header.Name = br.ReadString();
			header.SectionLength = br.ReadInt32();

			return header;
		}

		private static List<BinaryGameSection> ReadSections(BinaryReader br, byte sectionCount)
		{
			var r = new List<BinaryGameSection>();
			for (var i = 0; i < sectionCount; i++)
			{
				r.Add(ReadSection(br));
			}
			return r;
		}

		private void WriteSection(BinaryGameSection s, BinaryWriter bw)
		{
			// Write Section Header
			bw.Write(s.Header.Name);
			bw.Write(s.Header.SectionLength);

			// Write Body
			bw.Write(s.Body);
		}

		private void WriteSections(BinaryWriter bw)
		{
			foreach (var s in Sections)
			{
				WriteSection(s, bw);
			}
		}
	}
}