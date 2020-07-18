namespace TextEngine.Parsing.Syntax
{
    public class CharacterDefinitionNode : PropertyOnlyBasedCommand
    {
        public CharacterDefinitionNode(string name, PropertyList properties, BlockNode body) : base(name, properties, body)
        {
        }
    }
}