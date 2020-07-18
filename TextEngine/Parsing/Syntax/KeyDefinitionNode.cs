namespace TextEngine.Parsing.Syntax
{
    public class KeyDefinitionNode : PropertyOnlyBasedCommand
    {
        public KeyDefinitionNode(string name, PropertyList properties, BlockNode body) : base(name, properties, body)
        {
        }
    }
}