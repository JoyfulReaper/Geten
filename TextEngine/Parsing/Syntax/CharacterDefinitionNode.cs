using System.Collections.Generic;

namespace TextEngine.Parsing.Syntax
{
    public class CharacterDefinitionNode : PropertyOnlyBasedCommand
    {
        public CharacterDefinitionNode(string name, PropertyList properties) : base(name, properties)
        {
        }
    }
}