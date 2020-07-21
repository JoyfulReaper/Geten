using Geten.Core.Parsing;

namespace Geten.Parsers.Script.Syntax
{
    public class AddItemNode : TargetedNode
    {
        public AddItemNode(Token<SyntaxKind> action, Token<SyntaxKind> argument, Token<SyntaxKind> name, Token<SyntaxKind> from, Token<SyntaxKind> target) : base(action, argument, name, from, target)
        {
        }

        public override void Accept(IScriptVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}