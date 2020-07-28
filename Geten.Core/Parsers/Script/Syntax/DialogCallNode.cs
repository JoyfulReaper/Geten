using Geten.Core.Parsing;

namespace Geten.Core.Parsers.Script.Syntax
{
    public class DialogCallNode : SyntaxNode
    {
        public DialogCallNode(Token<SyntaxKind> dialogKeyword, Token<SyntaxKind> target)
        {
            DialogKeyword = dialogKeyword;
            Target = target;
        }

        public Token<SyntaxKind> DialogKeyword { get; }
        public Token<SyntaxKind> Target { get; }

        public override void Accept(IScriptVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}