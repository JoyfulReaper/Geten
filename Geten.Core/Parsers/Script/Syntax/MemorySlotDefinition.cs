using Geten.Core.Parsing;

namespace Geten.Core.Parsers.Script.Syntax
{
    public class MemorySlotDefinition : SyntaxNode
    {
        public MemorySlotDefinition(Token<SyntaxKind> keyword, Token<SyntaxKind> slotname, Token<SyntaxKind> equalsToken, SyntaxNode initialvalue)
        {
            KeywordToken = keyword;
            SlotnameToken = slotname;
            EqualsToken = equalsToken;
            ValueToken = initialvalue;
        }

        public Token<SyntaxKind> EqualsToken { get; }

        public Token<SyntaxKind> KeywordToken { get; }

        public Token<SyntaxKind> SlotnameToken { get; }

        public SyntaxNode ValueToken { get; }

        public override void Accept(IScriptVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}