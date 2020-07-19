using System.Collections.Generic;

namespace TextEngine.Parsing.Syntax
{
    public class PropertyList : Dictionary<Token<SyntaxKind>, SyntaxNode>
    {
        public object this[string key]
        {
            get
            {
                foreach (var kvp in this)
                {
                    if(kvp.Key.Text.ToLower().ToString() == key.ToLower())
                    {
                        return ((LiteralNode)kvp.Value).ValueToken.Value;
                    }
                }

                return null;
            }
        }
    }
}