using Geten.Runtime.IO;
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
            bf = GameBinaryBuilder.Build().AddSection("IndexTable", BitConverter.GetBytes(42));
        }

        [TestMethod]
        public void ReadBinaryFile_Should_Pass()
        {
            var ms = new MemoryStream();
            bf.Save(ms);

            var tmp = BinaryGameDefinitionFile.Load(new MemoryStream(ms.ToArray()));
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