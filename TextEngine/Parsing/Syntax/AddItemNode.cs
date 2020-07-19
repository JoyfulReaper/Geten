namespace TextEngine.Parsing.Syntax
{
    public class AddItemNode : TargetedNode
    {
        public AddItemNode(Token<SyntaxKind> action, Token<SyntaxKind> argument, Token<SyntaxKind> name, Token<SyntaxKind> from, Token<SyntaxKind> target) : base(action, argument, name, from, target)
        {
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}