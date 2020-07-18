namespace TextEngine.Parsing.Syntax
{
    public class RoomDefinitionNode : PropertyOnlyBasedCommand
    {
        public RoomDefinitionNode(string name, PropertyList properties, BlockNode body) : base(name, properties, body)
        {
        }

    }
}
