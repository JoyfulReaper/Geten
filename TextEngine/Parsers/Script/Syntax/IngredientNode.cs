using Geten.Core.Crafting;
using Geten.Core.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geten.Parsers.Script.Syntax
{
    public class IngredientNode : SyntaxNode
    {
        public IngredientNode(Ingredients ingredients)
        {
            Ingredients = ingredients;
        }

        public Ingredients Ingredients { get; }

        public override void Accept(IScriptVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
