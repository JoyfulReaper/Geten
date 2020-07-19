using System;
using TextEngine.MapItems;
using TextEngine.Parsing.Syntax;

namespace TextEngine.Parsing
{
    public class EvaluationVisitor : IVisitor
    {
        public void Visit(BlockNode block)
        {
            foreach (var node in block.Children)
            {
                node?.Accept(this);
            }
        }

        public void Visit(AddItemNode node)
        {
            throw new NotImplementedException();
        }

        public void Visit(CharacterDefinitionNode node)
        {
            throw new NotImplementedException();
        }

        public void Visit(AskForInputNode node)
        {
            throw new NotImplementedException();
        }

        public void Visit(CommandNode node)
        {
            throw new NotImplementedException();
        }

        public void Visit(DecreaseNode node)
        {
            throw new NotImplementedException();
        }

        public void Visit(IncreaseNode node)
        {
            throw new NotImplementedException();
        }

        public void Visit(DialogCallNode node)
        {
            throw new NotImplementedException();
        }

        public void Visit(EventSubscriptionNode node)
        {
            throw new NotImplementedException();
        }

        public void Visit(ItemDefinitionNode node)
        {
            throw new NotImplementedException();
        }

        public void Visit(KeyDefinitionNode node)
        {
            throw new NotImplementedException();
        }

        public void Visit(LiteralNode node)
        {
            throw new NotImplementedException();
        }

        public void Visit(MemorySlotDefinition node)
        {
            throw new NotImplementedException();
        }

        public void Visit(PlayNode node)
        {
            throw new NotImplementedException();
        }

        public void Visit(RemoveItemNode node)
        {
            throw new NotImplementedException();
        }

        public void Visit(RoomDefinitionNode node)
        {
            var name = node.NameToken.Value.ToString();
            var shortName = node.Properties["shortName"]?.ToString();
            var desc = node.Properties["description"]?.ToString();
            var lookDesc = node.Properties["lookDescription"]?.ToString();

            Room r = new Room(name, shortName, desc, lookDesc);

            TextEngine.AddRoom(r);
        }

        public void Visit(ExitDefinitionNode node)
        {
            var name = node.NameToken.Value.ToString();
            var locked = (bool)node.Properties["locked"];
            var visible = (bool)node.Properties["visible"];
            var toRoom = node.Properties["toRoom"].ToString();

            Room r = TextEngine.GetRoom(toRoom);

            Exit e = new Exit(name, r, locked, visible);
        }

        public void Visit(SetPropertyNode node)
        {
            throw new NotImplementedException();
        }

        public void Visit(TellNode node)
        {
            throw new NotImplementedException();
        }

        public void Visit(WeaponDefinitionNode node)
        {
            throw new NotImplementedException();
        }
       
    }
}