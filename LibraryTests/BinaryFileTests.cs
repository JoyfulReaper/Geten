using Geten.Core;
using Geten.Core.Factories;
using Geten.Runtime.Binary;
using Geten.Runtime.IO;
using Geten.Runtime.IO.Tables;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text;

namespace LibraryTests
{
	[TestClass]
	public class BinaryFileTests
	{
		private BinaryGameDefinitionFile _bf;

		[TestInitialize]
		public void Init()
		{
			if (!ObjectFactory.IsRegisteredFor<GameObject>())
			{
				ObjectFactory.Register<GameObjectFactory, GameObject>();
			}

			var metadata = new MetadataTable
			{
				["name"] = "Fork Zork",
				["version"] = "1.0.0.0"
			};

			_bf = GameBinaryBuilder.Build()
				.AddSection("Awnser", BitConverter.GetBytes(42))
				.AddTableSection(metadata)
				.AddStringSection("Definitions", File.ReadAllText("base.script"))
				.GetFile();
		}

		[TestMethod]
		public void ReadBinaryFile_Should_Pass()
		{
			var ms = new MemoryStream();
			_bf.Save(ms);

			var tmp = BinaryGameDefinitionFile.Load(new MemoryStream(ms.ToArray()));
			var metadata = tmp.GetTable<MetadataTable>();
			var objs = tmp.GetBodyOfSection("Definitions");
			var script = Encoding.ASCII.GetString(objs);

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