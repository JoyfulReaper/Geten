using Geten.Core;
using Geten.Core.Crafting;
using Geten.Core.Parsing.Diagnostics;
using Geten.Factories;
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
            Targeted(node);
        }

        public void Visit(CharacterDefinitionNode node)
        {
            var asWhat = node.AsWhatToken.Text?.ToString();
            var name = node.NameToken.Value?.ToString();

            Character character = null;

            if (asWhat == "player") // It's the player
            {
                character = GameObject.Create<Player>(name, node.Properties);

                TextEngine.Player = (Player)character;
            }
            else if (asWhat == "npc") // It's an NPC
            {
                character = GameObject.Create<NPC>(name, node.Properties);
            }
            else
            {
                Diagnostics.ReportBadPlayerCharacter(name);
            }

            character.Inventory.Capacity = character.GetProperty<int>("inventorysize");
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
            var name = node.NameToken.Value?.ToString();
            var container = (bool)(node.Properties["container"] ?? false);

            Item item;
            if (container)
            {
                item = GameObject.Create<ContainerItem>(name, node.Properties);
            }
            else
            {
                item = GameObject.Create<Item>(name, node.Properties);
            }

            dynamic i = item;

            if (i.location == null)
                return;

            if (i.location == "player")
            {
                // Add to player inventory
                TextEngine.Player.Inventory.AddItem(item, i.quantity);
            }
            else if (SymbolTable.Contains<Room>(i.location))
            {
                // Add to Room Inventory
                SymbolTable.GetInstance<Room>(i.location).Inventory.AddItem(item, i.quantity);
            }
            else if (SymbolTable.Contains<NPC>(i.location))
            {
                // Add to NPC inventory
                SymbolTable.GetInstance<NPC>(i.location).Inventory.AddItem(item, i.quantity);
            }
            else if (SymbolTable.Contains<ContainerItem>(i.location))
            {
                SymbolTable.GetInstance<ContainerItem>(i.location).Inventory.AddItem(item, i.quantity);
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
            Targeted(node);
        }

        public void Visit(RoomDefinitionNode node)
        {
            var name = node.NameToken.Value.ToString();

            dynamic r = GameObject.Create<Room>(name, node.Properties);
            if (r.startRoom)
                TextEngine.StartRoom = r;
        }

        public void Visit(ExitDefinitionNode node)
        {
            var name = node.NameToken.Value.ToString();
            var side = node.Properties["side"]?.ToString();
            var toRoom = node.Properties["toRoom"]?.ToString();
            var fromRoom = node.Properties["fromRoom"]?.ToString();

            Direction dirSide = TextEngine.GetDirectionFromChar(Char.ToUpper(side[0]));
            Room to = SymbolTable.GetInstance<Room>(toRoom);
            Room from = SymbolTable.GetInstance<Room>(fromRoom);

            var exit = GameObject.Create<Exit>(name, node.Properties);

            from.SetSide(dirSide, exit);
        }

        public void Visit(SetPropertyNode node)
        {
            throw new NotImplementedException();
        }

        public void Visit(TellNode node)
        {
            TextEngine.AddMessage(node.MessageToken.Value.ToString());
        }

        public void Visit(WeaponDefinitionNode node)
        {
            throw new NotImplementedException();
        }

        public void Visit(RecipeBookDefinition node)
        {
            var rb = ObjectFactory.Create<RecipeBook>(node.NameToken.Value.ToString());

            foreach (RecipeDefinitionNode r in node.Recipes)
            {
                var recipe = new Recipe(r.NameToken.Value.ToString(), r.Ingredients, SymbolTable.GetInstance<Item>(r.OuputToken.Value.ToString()));
                rb.Add(recipe);
            }
        }

        public void Visit(RecipeDefinitionNode node)
        {
            throw new NotImplementedException();
        }

        private void ChangeQuantity<T>(T node) where T : ChangeQuantityNode
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
                    }
                    else
                    {
                        var instanceTarget = SymbolTable.GetInstance<NPC>(instance);
                        if (instanceTarget != null)
                        {
                            if (increase)
                                instanceTarget.Health += amount;
                            else
                                instanceTarget.Health -= amount;
                        }
                        else
                        {
                            Diagnostics.ReportBadNPC(instance);
                        }
                    }

                    break;

                default:
                    break;
            }
        }

        private void Targeted<T>(T node)
                    where T : TargetedNode
        {
            var addWhat = node.Argument?.ToString();
            var name = node.Name.Value?.ToString();
            var target = node.Target.Value?.ToString();
            bool add = node.GetType() == typeof(AddItemNode);

            Item item = SymbolTable.GetInstance<Item>(name);

            if (target == "player")
            {
                // Add item to Players's inv
                if (add)
                    TextEngine.Player?.Inventory.AddItem(item);
                else
                    TextEngine.Player?.Inventory.RemoveItem(item);
            }
            else
            {
                if (SymbolTable.Contains<Room>(target))
                {
                    // Add item to Room's inv
                    if (add)
                        SymbolTable.GetInstance<Room>(target).Inventory.AddItem(item);
                    else
                        SymbolTable.GetInstance<Room>(target).Inventory.RemoveItem(item);
                }
                else if (SymbolTable.Contains<NPC>(target))
                {
                    // Get all NPCs and check for one with name
                    if (add)
                        SymbolTable.GetInstance<NPC>(target).Inventory.AddItem(item);
                    else
                        SymbolTable.GetInstance<NPC>(target).Inventory.RemoveItem(item);
                }
                else
                {
                    // Get all Container Items and check for one with name
                    try
                    {
                        if (add)
                            SymbolTable.GetInstance<ContainerItem>(target).Inventory.AddItem(item);
                        else
                            SymbolTable.GetInstance<ContainerItem>(target).Inventory.RemoveItem(item);
                    }
                    catch
                    {
                        Diagnostics.ReportBadTargetInventory(target);
                    }
                }
            }
        }
    }
}