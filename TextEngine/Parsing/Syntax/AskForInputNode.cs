using System.Collections.Generic;

namespace TextEngine.Parsing.Syntax
{
    public class AskForInputNode : SyntaxNode
    {

        public AskForInputNode(Token askKeyword, Token forKeyword, Token message, Token toKeyword, Token slotname)
        {
            AskKeyword = askKeyword;
            ForKeyword = forKeyword;
            ToKeyword = toKeyword;
            Slotname = slotname;
            Message = message;
        }

        public Token AskKeyword { get; }
        public Token ForKeyword { get; }
        public Token ToKeyword { get; }
        public Token Slotname { get; }
        public Token Message { get; }
    }
}