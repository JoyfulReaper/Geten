using Geten.Core.Parsing.Text;
using Geten.Parsers.Script;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Linq;

namespace LibraryTests
{
    [TestClass]
    public class LexerTests
    {
        [TestMethod]
        public void Lex_Symbol_Should_Pass()
        {
            var l = new ScriptLexer(SourceText.From("@name"));
            var token = l.GetAllTokens().First();

            Assert.AreEqual(token.Kind, SyntaxKind.Symbol);
            Assert.AreEqual(token.Value, "name");
        }

        [TestMethod]
        public void Lexer_Should_Pass()
        {
            var src = "memoryslot \"name\"\non \"setup\" ask for \"Please Tell me your name: \" to \"name\"";
            var lexer = new ScriptLexer(SourceText.From(src, "tests.script"));

            var tokens = lexer.GetAllTokens();

            foreach (var t in tokens)
            {
                Debug.WriteLine(t);
            }

            AssertNoDiagnostics(lexer);
        }

        private void AssertNoDiagnostics(ScriptLexer lexer)
        {
            if (lexer.Diagnostics.Any())
            {
                throw new Exception(lexer.Diagnostics.First().ToString());
            }
        }
    }
}