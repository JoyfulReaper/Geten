using Geten.Core;
using Geten.Core.Parsing;
using Geten.Parsers.Script;
using Geten.Parsers.Script.Syntax;
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
            Assert.IsTrue("hElLO WoRld" == (CaseInsensitiveString)"hello world");
        }

        [TestMethod]
        public void GameObject_Initializer_Should_Pass()
        {
            var go = new TestObject {
                { "name", "friend" },
                { "attackable", false }
            };

            Assert.AreEqual(go.PropertyCount, 3);
        }
    }

    internal class TestObject : GameObject
    {
        public TestObject() : base("test", "nothing")
        {
        }
    }
}