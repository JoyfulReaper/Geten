using Geten.Core;
using Geten.Core.Factories;
using Geten.Core.GameObjects;
using Geten.Core.MapItems;
using Geten.Core.Parsers.Script;
using Geten.Core.Parsers.Script.Syntax;
using Geten.Core.Parsing;
using Geten.TextProcessing;
using Geten.TextProcessing.Synonyms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace LibraryTests
{
	[TestClass]
	public class LibTests
	{
		[TestMethod]
		public void BlockNode_Descendants_Should_Pass()
		{
			var root = new BlockNode(new List<SyntaxNode>
			{
				new IncreaseNode(new Token<SyntaxKind>(SyntaxKind.Keyword, 0, "nothing", null), null, null, null, null, null),
				new BlockNode(new List<SyntaxNode>
				{
					new BlockNode(new List<SyntaxNode> {
						new DecreaseNode(new Token<SyntaxKind>(SyntaxKind.Keyword, 0, "blub", null), null, null, null, null, null),
					}),
					new CommandNode(new Token<SyntaxKind>(SyntaxKind.Keyword, 0, "command", null), new Token<SyntaxKind>(SyntaxKind.String, 0, "look chest", null)),
				})
			});

			var result = root.Descendants<CommandNode>();

			Assert.AreEqual(result.First().KeywordToken.Text.ToString(), "command");
		}

		[TestMethod]
		public void Compare_CaseInsensitiveString_Should_Pass()
		{
			Assert.IsTrue("hElLO WoRld" == (CaseSensisitiveString)"hello world");
		}

		[TestMethod]
		public void GameObject_Initializer_Should_Pass()
		{
			var go = new TestObject {
				{ "name", "friend" },
				{ "attackable", false }
			};

			Assert.AreEqual(go.PropertyCount, 2);
		}

		[TestMethod]
		public void Get_All_Rooms_Should_Pass()
		{
			SymbolTable.ClearAllSymbols();
			GameObject.Create<Room>("Test Room");
			GameObject.Create<Room>("Another Test Room");
			var rooms = TextEngine.GetAllRooms();
			Assert.AreEqual(rooms.Count(), 2);
		}

		[TestInitialize]
		public void Init()
		{
			if (!ObjectFactory.IsRegisteredFor<GameObject>())
			{
				ObjectFactory.Register<GameObjectFactory, GameObject>();
			}
		}

		[TestMethod]
		public void SetDynamicProperty_On_GameObject_Should_Pass()
		{
			dynamic go = GameObject.Create<Item>("empty");
			go.isidiot = true;

			Assert.AreEqual(go.isidiot, true);
		}

		[TestMethod]
		public void TextProcessor_Should_Pass()
		{
			var parser = new Parser();
			parser.Nouns.Add("key", "key");
			parser.Nouns.Add("floor", "floor");

			var command = parser.ParseCommand("Grab Key from floor");

			Assert.AreEqual(VerbCodes.Take, command.Verb);
			Assert.AreEqual("key", command.Noun);
			Assert.AreEqual(PropositionEnum.From, command.Preposition);
			Assert.AreEqual("floor", command.Noun2);
			Assert.AreEqual("grab key from floor", command.FullTextCommand);
		}
	}

	internal class TestObject : GameObject
	{
	}
}