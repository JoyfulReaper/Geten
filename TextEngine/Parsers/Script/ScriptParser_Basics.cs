using Geten.Core.Parsing;
using Geten.Parsers.Script.Syntax;

namespace Geten.Parsers.Script
{
    public partial class ScriptParser
    {
        public bool AcceptKeyword(string keyword, out Token<SyntaxKind> token)
        {
            token = Peek(0);
            if (token.Kind == SyntaxKind.Keyword && token.Text == keyword)
            {
                token = MatchKeyword(keyword);
                return true;
            }

            token = new Token<SyntaxKind>(SyntaxKind.BadToken, -1, null, null);
            return false;

            /*
            try
            {
                token = MatchKeyword(keyword);
                return true;
            }
            catch
            {
                token = new Token<SyntaxKind>(SyntaxKind.BadToken, -1, null, null);
                return false;
            }
            */
        }

        public bool MatchCurrentKeyword(string keyword)
        {
            return Current.Kind == SyntaxKind.Keyword && Current.Text == keyword;
        }

        public Token<SyntaxKind> MatchKeyword(string keyword)
        {
            var keywordToken = MatchToken(SyntaxKind.Keyword);
            if (keywordToken.Text == keyword)
            {
                return keywordToken;
            }

            Diagnostics.ReportUnexpectedKeyword(Current.Span, keywordToken, keyword);

            return null;
        }

        public bool MatchNextKeyword(string keyword)
        {
            return Peek(0).Kind == SyntaxKind.Keyword && Peek(0).Text == keyword;
        }

        public SyntaxNode ParseBlock()
        {
            var members = ParseMembers();

            return new BlockNode(members);
        }
    }
}