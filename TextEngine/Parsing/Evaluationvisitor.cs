﻿using System;
using TextEngine.MapItems;
using TextEngine.Parsing.Diagnostics;
using TextEngine.Parsing.Syntax;

namespace TextEngine.Parsing
{
    public class EvaluationVisitor : IVisitor
    {
        public EvaluationVisitor(DiagnosticBag parent)
        {
            Diagnostics.AddRange(parent);
        }

        DiagnosticBag Diagnostics = new DiagnosticBag();

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

            // need to add token for as player or as npc
            var name = node.NameToken.Value?.ToString();
            var desciption = node.Properties["description"]?.ToString();
            var maxHealth = (int)(node.Properties["maxHealth"] ?? 100);
            var health = (int)(node.Properties["health"] ?? 100);
            var money = (int)(node.Properties["money"] ?? 0); // not used right now

            if(false) // It's the player
            {
                Player player = new Player(name, desciption, health, maxHealth);
                TextEngine.Player = player;
            } else // It's an NPC
            {
                NPC npc = new NPC(name, desciption, maxHealth, health);
                TextEngine.AddNPC(npc);
            }

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
            var side = node.Properties["side"]?.ToString();
            var toRoom = node.Properties["toRoom"]?.ToString();
            var fromRoom = node.Properties["fromRoom"]?.ToString();

            Direction dirSide = TextEngine.GetDirectionFromChar(Char.ToUpper(side[0])); 
            Room to = TextEngine.GetRoom(toRoom);
            Room from = TextEngine.GetRoom(fromRoom);

            Exit exit = new Exit(name, to, locked, visible);
            to.SetSide(dirSide, exit);
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