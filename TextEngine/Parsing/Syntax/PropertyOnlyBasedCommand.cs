﻿namespace TextEngine.Parsing.Syntax
{
    public abstract class PropertyOnlyBasedCommand : SyntaxNode
    {
        public PropertyOnlyBasedCommand(Token<SyntaxKind> keywordToken, Token<SyntaxKind> nameToken, Token<SyntaxKind> withToken, PropertyList properties, BlockNode body)
        {
            Properties = properties;
            Body = body;
            KeywordToken = keywordToken;
            NameToken = nameToken;
            WithToken = withToken;
        }

        public PropertyList Properties { get; }
        public BlockNode Body { get; }
        public Token<SyntaxKind> KeywordToken { get; }
        public Token<SyntaxKind> NameToken { get; }
        public Token<SyntaxKind> WithToken { get; }
    }
}