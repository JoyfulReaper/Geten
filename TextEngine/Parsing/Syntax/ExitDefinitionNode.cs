using System;
using System.Collections.Generic;
using System.Text;

namespace TextEngine.Parsing.Syntax
{
    public class ExitDefinitionNode : PropertyOnlyBasedCommand
    {
        public ExitDefinitionNode(Token<SyntaxKind> keywordToken, Token<SyntaxKind> nameToken, Token<SyntaxKind> withToken, PropertyList properties, BlockNode body) : base(keywordToken, nameToken, withToken, properties, body)
        {
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
