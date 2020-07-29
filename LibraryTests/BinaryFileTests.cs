using Geten.Core.Memory;
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
            bf = new BinaryGameDefinitionFile();
            bf.Header.SectionCount = 1;
            var s = new BinaryGameSection();
            s.Header.Name = "ObjectTable";
            s.Header.SectionLength = 4;

            s.Body = BitConverter.GetBytes(42);

            bf.Sections.Add(s);
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