using Geten.Core;
using Geten.Core.Parsing;
using System.Collections.Generic;

namespace Geten.Parsers.Script.Syntax
{
    public class PropertyList : Dictionary<Token<SyntaxKind>, SyntaxNode>
    {
        public object this[CaseInsensitiveString key]
        {
            get
            {
                foreach (var kvp in this)
                {
                    if(kvp.Key.Text == key)
                    {
                        return ((LiteralNode)kvp.Value).ValueToken.Value;
                    }
                }

                return null;
            }
        }
    }
}