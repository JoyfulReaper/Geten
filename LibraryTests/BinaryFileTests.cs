using Geten.Runtime;
using Geten.Runtime.IO;
using Geten.Runtime.Tables;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace LibraryTests
{
	[TestClass]
	public class BinaryFileTests
	{
		private BinaryGameDefinitionFile _bf;

		[TestInitialize]
		public void Init()
		{
			var metadata = new MetadataTable
			{
				{ "name", "Fork Zork" },
				{ "version", "1.0.0.0" }
			};

			_bf = GameBinaryBuilder.Build()
				.AddSection("Awnser", BitConverter.GetBytes(42))
				.AddTableSection(metadata)
				.GetFile();
		}

		[TestMethod]
		public void ReadBinaryFile_Should_Pass()
		{
			var ms = new MemoryStream();
			_bf.Save(ms);

			var tmp = BinaryGameDefinitionFile.Load(new MemoryStream(ms.ToArray()));
			var metadata = tmp.GetTable<MetadataTable>();
			Assert.AreEqual(metadata.Count, 2);
		}

		[TestMethod]
		public void SaveBinaryFile_Should_Pass()
		{
			var ms = new MemoryStream();
			_bf.Save(ms);

			var raw = ms.ToArray();
			Assert.AreEqual(raw.Length, 73);
		}
	}
}