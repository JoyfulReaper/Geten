namespace TextEngine.Parsing.Syntax
{
    public class WeaponDefinitionNode : PropertyOnlyBasedCommand
    {
        public WeaponDefinitionNode(string name, PropertyList properties, BlockNode body) : base(name, properties, body)
        {
        }
    }
}