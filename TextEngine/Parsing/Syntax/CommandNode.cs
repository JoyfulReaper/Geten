﻿namespace TextEngine.Parsing.Syntax
{
    public class CommandNode : SyntaxNode
    {
        public CommandNode(Token<SyntaxKind> keywordToken, Token<SyntaxKind> commandToken)
        {
            KeywordToken = keywordToken;
            CommandToken = commandToken;
        }

        public Token<SyntaxKind> KeywordToken { get; }
        public Token<SyntaxKind> CommandToken { get; }
    }
}
