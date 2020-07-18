namespace TextEngine.Parsing.Syntax
{
    class CommandNode : SyntaxNode
    {
        public string Command { get; set; }
        public CommandNode(string command)
        {
            Command = command; 
        }
    }
}
