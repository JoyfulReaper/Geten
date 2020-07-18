using System.Collections.Generic;

namespace TextEngine.Parsing.Syntax
{
    public class KeyDefinitionNode : PropertyOnlyBasedCommand
    {
        public KeyDefinitionNode(string name, PropertyList properties) : base(name, properties)
        {
        }
    }
}