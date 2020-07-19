using System;
using System.Collections.Generic;
using System.Text;

namespace TextEngine.Parsing.Syntax
{
    public class ExitDefinitionNode : SyntaxNode
    {
        public ExitDefinitionNode(Token<SyntaxKind> ExitKeyword, Token<SyntaxKind> NameToken, PropertyList properties)
        {
            this.ExitKeyword = ExitKeyword;
            this.NameToken = NameToken;
            Properties = properties;
        }

        public Token<SyntaxKind> ExitKeyword { get; }
        public Token<SyntaxKind> NameToken { get; }
        public PropertyList Properties { get; }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
