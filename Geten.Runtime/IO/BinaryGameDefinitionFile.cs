using Geten.Core;
using System.Collections.Generic;
using System.IO;

namespace Geten.Runtime.IO
{
    public class BinaryGameDefinitionFile
    {
        public BinaryGameFileHeader Header { get; set; } = new BinaryGameFileHeader();
        public List<BinaryGameSection> Sections { get; set; } = new List<BinaryGameSection>();

        public static BinaryGameDefinitionFile Load(Stream strm)
        {
            var br = new BinaryReader(strm);
            var result = new BinaryGameDefinitionFile();
            result.Header = ReadHeader(br);
            result.Sections = ReadSections(br, result.Header.SectionCount);

            br.Close();

            return result;
        }

        public byte[] GetBodyOfSection(CaseInsensitiveString name)
        {
            foreach (var s in Sections)
            {
                if (s.Header.Name == name)
                {
                    return s.Body;
                }
            }

            throw new System.Exception("No Section called '{name}' found");
        }

        public void Save(Stream str)
        {
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
            for (int i = 0; i < sectionCount; i++)
            {
                r.Add(ReadSection(br));
            }
            return r;
        }
    }
}