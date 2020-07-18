namespace TextEngine.Parsing.Syntax
{
    public class ItemDefinitionNode : PropertyOnlyBasedCommand
    {
        public ItemDefinitionNode(string name, PropertyList properties, BlockNode body) : base(name, properties, body)
        {
        }
    }
}