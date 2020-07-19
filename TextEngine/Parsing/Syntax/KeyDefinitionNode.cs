﻿namespace TextEngine.Parsing.Syntax
{
    public class KeyDefinitionNode : PropertyOnlyBasedCommand
    {
        public KeyDefinitionNode(Token<SyntaxKind> keywordToken, Token<SyntaxKind> nameToken, Token<SyntaxKind> withToken, PropertyList properties, BlockNode body) : base(keywordToken, nameToken, withToken, properties, body)
        {
        }
    }
}