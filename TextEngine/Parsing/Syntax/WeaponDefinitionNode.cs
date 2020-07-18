using System.Collections.Generic;

namespace TextEngine.Parsing.Syntax
{
    public class WeaponDefinitionNode : PropertyOnlyBasedCommand
    {
        public WeaponDefinitionNode(string name, PropertyList properties) : base(name, properties)
        {
        }
    }
}