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
        private BinaryGameDefinitionFile bf;

        [TestInitialize]
        public void Init()
        {
            var metadata = new MetadataTable();
            metadata.Add("name", "Fork Zork");
            metadata.Add("version", "1.0.0.0");

            bf = GameBinaryBuilder.Build()
                .AddSection("Awnser", BitConverter.GetBytes(42))
                .AddTableSection("MetadataTable", metadata)
                .GetFile();
        }

        [TestMethod]
        public void ReadBinaryFile_Should_Pass()
        {
            var ms = new MemoryStream();
            bf.Save(ms);

            var tmp = BinaryGameDefinitionFile.Load(new MemoryStream(ms.ToArray()));
            var metadata = tmp.GetTable<MetadataTable>("MetadataTable");
        }

        [TestMethod]
        public void SaveBinaryFile_Should_Pass()
        {
            var ms = new MemoryStream();
            bf.Save(ms);

            var raw = ms.ToArray();
        }
    }
}