using Geten.Core.Parsing;

namespace Geten.Parsers.Script.Syntax
{
    public class AskForInputNode : SyntaxNode
    {

        public AskForInputNode(Token<SyntaxKind> askKeyword, Token<SyntaxKind> forKeyword, Token<SyntaxKind> message, Token<SyntaxKind> toKeyword, Token<SyntaxKind> slotname)
        {
            AskKeyword = askKeyword;
            ForKeyword = forKeyword;
            ToKeyword = toKeyword;
            Slotname = slotname;
            Message = message;
        }

        public Token<SyntaxKind> AskKeyword { get; }
        public Token<SyntaxKind> ForKeyword { get; }
        public Token<SyntaxKind> ToKeyword { get; }
        public Token<SyntaxKind> Slotname { get; }
        public Token<SyntaxKind> Message { get; }

        public override void Accept(IScriptVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}