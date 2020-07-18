namespace TextEngine.Parsing.Syntax
{
    public abstract class PropertyOnlyBasedCommand : SyntaxNode
    {
        public PropertyOnlyBasedCommand(string name, PropertyList properties)
        {
            Name = name;
            Properties = properties;
        }

        public string Name { get; }
        public PropertyList Properties { get; }
    }
}