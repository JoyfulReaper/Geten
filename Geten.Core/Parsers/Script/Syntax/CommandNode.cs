using Geten.Core.Parsing;

namespace Geten.Core.Parsers.Script.Syntax
{
    public class CommandNode : SyntaxNode
    {
        public CommandNode(Token<SyntaxKind> keywordToken, Token<SyntaxKind> commandToken)
        {
            KeywordToken = keywordToken;
            CommandToken = commandToken;
        }

        public Token<SyntaxKind> CommandToken { get; }
        public Token<SyntaxKind> KeywordToken { get; }

        public override void Accept(IScriptVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}