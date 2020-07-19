using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Linq;
using TextEngine.Parsing;
using TextEngine.Parsing.Text;

namespace LibraryTests
{
    [TestClass]
    public class LexerTests
    {
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