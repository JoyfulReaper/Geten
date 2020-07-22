﻿using Geten.Core;
using Geten.Core.Parsing.Diagnostics;
using Geten.GameObjects;
using Geten.MapItems;
using Geten.Parsers.Script.Syntax;
using System;

namespace Geten.Parsers.Script
{
    public class EvaluationVisitor : IScriptVisitor
    {
        private readonly DiagnosticBag Diagnostics;

        public EvaluationVisitor(DiagnosticBag parent)
        {
            Diagnostics = parent;
        }

        public void Visit(BlockNode block)
        {
            foreach (var node in block.Children)
            {
                node?.Accept(this);
            }
        }

        public void Visit(AddItemNode node)
        {
            var addWhat = node.Argument?.ToString();
            var name = node.Name.Value?.ToString();
            var target = node.Target.Value?.ToString();

            Item item = SymbolTable.GetInstance<Item>(name);

            if (target == "player")
            {
                // Add item to Players's inv
                TextEngine.Player?.Inventory.AddItem(item);
            }
            else
            {
                if (TextEngine.RoomExists(target))
                {
                    // Add item to Room's inv
                    SymbolTable.GetInstance<Room>(target).Inventory.AddItem(item);
                }
                else if (TextEngine.NpcExists(target))
                {
                    // Get all NPCs and check for one with name
                    SymbolTable.GetInstance<NPC>(target).Inventory.AddItem(item);
                }
                else
                {
                    // Get all Container Items and check for one with name
                    try
                    {
                        SymbolTable.GetInstance<ContainerItem>(target).Inventory.AddItem(item);
                    }
                    catch
                    {
                        Diagnostics.ReportBadTargetInventory(target);
                    }
                }
            }
        }

        public void Visit(CharacterDefinitionNode node)
        {
            var asWhat = node.AsWhatToken.Text?.ToString();
            var name = node.NameToken.Value?.ToString();
            var description = node.Properties["description"]?.ToString();
            var maxHealth = (int)(node.Properties["maxHealth"] ?? 100);
            var health = (int)(node.Properties["health"] ?? 100);
            var money = (int)(node.Properties["money"] ?? 0); // not used right now

            if (asWhat == "player") // It's the player
            {
                Player player = new Player(name, description, health, maxHealth);
                TextEngine.Player = player;

                SymbolTable.Add(name, player);
            }
            else if (asWhat == "npc") // It's an NPC
            {
                NPC npc = new NPC(name, description, maxHealth, health);
                TextEngine.AddNPC(npc);

                //SymbolTable.Add(name, npc); // AddNPC() does this now, hopefully that is the "correct" thing to do
            }
            else
            {
                Diagnostics.ReportBadPlayerCharacter(name);
            }
        }

        public void Visit(AskForInputNode node)
        {
            throw new NotImplementedException();
        }

        public void Visit(CommandNode node)
        {
            CommandProccessor.ProcessCommand((string)node.CommandToken.Value);
        }

        public void Visit(DecreaseNode node)
        {
            ChangeQuantity(node);
        }

        public void Visit(IncreaseNode node)
        {
            ChangeQuantity(node);
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
            // Need to have a way to determine where to put the item and if the item is a containter or not - Done
            // Need to figure out if the location is a room or another ContainerItem, then add to that inventory
            var name = node.NameToken.Value?.ToString();
            var pluralName = node.Properties["pluralName"]?.ToString(); // null is okay
            var obtainable = (bool)(node.Properties["obtainable"] ?? true);
            var visible = (bool)(node.Properties["visible"] ?? true);
            var desc = node.Properties["description"]?.ToString();
            var location = node.Properties["location"]?.ToString();
            var container = (bool)(node.Properties["container"] ?? false);

            Item item;
            if (container)
            {
                var capacity = (int)(node.Properties["capacity"] ?? 0);
                item = new ContainerItem(name, pluralName, desc, visible, obtainable, capacity);
            }
            else
            {
                item = new Item(name, pluralName, desc, visible, obtainable);
            }

            SymbolTable.Add(name, item);

            Room r = TextEngine.GetRoom(location);
            if (location != null)
            {
                if (location == "player")
                {
                    TextEngine.Player.Inventory.AddItem(item);
                }
                else
                {
                    if (TextEngine.RoomExists(location))
                    {
                        r.Inventory.AddItem(item);
                    }
                    else
                    {
                        // Get all Rooms and see if there is a ContainerItem with name name
                        // Get all NPCs and see if there is one with name
                    }
                }
            }
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
            //SymbolTable.Add(name, r); //TextEngine.AddRoom does this
            TextEngine.AddRoom(r);
        }

        public void Visit(ExitDefinitionNode node)
        {
            var name = node.NameToken.Value.ToString();
            var locked = (bool)(node.Properties["locked"] ?? false);
            var visible = (bool)(node.Properties["visible"] ?? true);
            var side = node.Properties["side"]?.ToString();
            var toRoom = node.Properties["toRoom"]?.ToString();
            var fromRoom = node.Properties["fromRoom"]?.ToString();

            Direction dirSide = TextEngine.GetDirectionFromChar(Char.ToUpper(side[0]));
            Room to = TextEngine.GetRoom(toRoom);
            Room from = TextEngine.GetRoom(fromRoom);

            Exit exit = new Exit(name, to, locked, visible);
            from.SetSide(dirSide, exit);
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

        private void ChangeQuantity<T>(T node)
                                                                                                                    where T : ChangeQuantityNode
        {
            bool increase = node.GetType() == typeof(IncreaseNode);
            var target = node.Target.Text?.ToString();
            var amount = (int)node.Amount.Value;
            var instance = node.Instance.Value?.ToString();

            switch (target)
            {
                case "health":
                    if (instance == "player")
                    {
                        if (increase)
                        {
                            TextEngine.Player.Health += amount;
                        }
                        else
                        {
                            TextEngine.Player.Health -= amount;
                        }
                    } else
                    {
                        var instanceTarget = TextEngine.GetNPC(instance);
                        if(instanceTarget != null)
                        {
                            if (increase)
                                instanceTarget.Health += amount;
                            else
                                instanceTarget.Health -= amount;
                        } else
                        {
                            Diagnostics.ReportBadNPC(instance);
                        } 
                    }

                    break;

                default:
                    break;
            }
        }
    }
}