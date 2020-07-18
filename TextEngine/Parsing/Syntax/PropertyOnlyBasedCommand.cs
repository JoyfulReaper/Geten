namespace TextEngine.Parsing.Syntax
{
    public abstract class PropertyOnlyBasedCommand : SyntaxNode
    {
        public PropertyOnlyBasedCommand(string name, PropertyList properties, BlockNode body)
        {
            Name = name;
            Properties = properties;
            Body = body;
        }

        public string Name { get; }
        public PropertyList Properties { get; }
        public BlockNode Body { get; }
    }
}