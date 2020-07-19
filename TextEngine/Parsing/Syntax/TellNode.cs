using System.Collections.Generic;

namespace TextEngine.Parsing.Syntax
{
    public class TellNode : SyntaxNode
    {
        public TellNode(string messageToken)
        {
            MessageToken = messageToken;
        }

        public string MessageToken { get; }
    }
}