using Geten;
using Geten.Core;
using Geten.GameObjects;
using Geten.MapItems;
using Geten.Parsers.Script;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace LibraryTests
{
    [TestClass]
    public class EvaluationTests
    {
        [TestMethod]
        public void Evaluate_Increase_Player_Health_Should_Pass()
        {
            var player = new Player("TestPlayer1", "He likes to test!", 90, 100);
            TextEngine.Player = player;

            var src = "increase health of 'player' by 10";
            var p = new ScriptParser();
            var r = p.Parse(src);

            r.Accept(new EvaluationVisitor(p.Diagnostics));
            AssertNoDiagnostics(p);
            Assert.IsTrue(Geten.TextEngine.Player.Health == 100);
        }

        [TestMethod]
        public void Evaluate_Increase_NPC_Health_Should_Pass()
        {
            var npc = new NPC("TestNPC1", "He likes to test", 90, 100);
            TextEngine.AddNPC(npc);

            var src = "increase health of 'TestNPC1' by 10";
            var p = new ScriptParser();
            var r = p.Parse(src);

            r.Accept(new EvaluationVisitor(p.Diagnostics));
            AssertNoDiagnostics(p);
            Assert.IsTrue(Geten.TextEngine.GetNPC("TestNPC1").Health == 100);
        }

        [TestMethod]
        public void Evaluate_Decrease_Player_Health_Should_Pass()
        {
            var player = new Player("TestPlayer2", "He likes to test!", 90, 100);
            TextEngine.Player = player;

            var src = "decrease health of 'player' by 10";
            var p = new ScriptParser();
            var r = p.Parse(src);

            r.Accept(new EvaluationVisitor(p.Diagnostics));
            AssertNoDiagnostics(p);
            Assert.IsTrue(Geten.TextEngine.Player.Health == 80);
        }

        [TestMethod]
        public void Evaluate_Decrease_NPC_Health_Should_Pass()
        {
            var npc = new NPC("TestNPC2", "He likes to test", 90, 100);
            TextEngine.AddNPC(npc);

            var src = "decrease health of 'TestNPC2' by 10";
            var p = new ScriptParser();
            var r = p.Parse(src);

            r.Accept(new EvaluationVisitor(p.Diagnostics));
            AssertNoDiagnostics(p);
            Assert.IsTrue(Geten.TextEngine.GetNPC("TestNPC2").Health == 80);
        }

        [TestMethod]
        public void Evaluate_Decrease_Bad_NPC_Health_Should_Pass()
        {
            var src = "decrease health of 'NOTestNPC' by 10";
            var p = new ScriptParser();
            var r = p.Parse(src);
            try
            {
                r.Accept(new EvaluationVisitor(p.Diagnostics));
                AssertNoDiagnostics(p);
            } catch (Exception e)
            {
                Assert.AreEqual(e.Message, "NPC 'NOTestNPC' is not valid");
            }
        }

        [TestMethod]
        public void Evaluate_Add_Item_BadTarget_Should_Pass()
        {
            var src = "item 'Book' with pluralName 'Books' and obtainable true and visible true and description 'you can read it' end end add item 'Book' to 'badTarget'";
            var p = new ScriptParser();
            var r = p.Parse(src);
            try
            {
                r.Accept(new EvaluationVisitor(p.Diagnostics));
                AssertNoDiagnostics(p);
            } catch (Exception e)
            {
                Assert.AreEqual(e.Message, "Target 'badTarget' is not a valid Room, NPC or ContainerItem");
            }
            
            
        }

        [TestMethod]
        public void Evaluate_Add_Item_to_ContainerItem_Should_Pass()
        {
            ContainerItem ci = new ContainerItem("Brown Bag", "It has a cookie in it", true, true);
            SymbolTable.Add(ci.Name, ci);
            string src = "item 'cookie' with description 'chocolate chip' end end add item 'cookie' to 'Brown Bag'";

            ScriptParser p = new ScriptParser();
            var res = p.Parse(src);
            res.Accept(new EvaluationVisitor(p.Diagnostics));

            AssertNoDiagnostics(p);
        }

        [TestMethod]
        public void Evaluate_Add_Item_to_NPC_Should_Pass()
        {
            NPC npc = new NPC("Some Guy", "He looks nice", 100, 100);
            Item item = new Item("Bag of Chips", "They look crunchy!");
            SymbolTable.Add(item.Name, item);
            TextEngine.AddNPC(npc);
            string src = "add item 'Bag of Chips' to 'Some Guy'";
            ScriptParser p = new ScriptParser();
            var res = p.Parse(src);
            res.Accept(new EvaluationVisitor(p.Diagnostics));

            AssertNoDiagnostics(p);
        }

        [TestMethod]
        public void Evaluate_Add_Item_to_Room_Should_Pass()
        {
            Room r = new Room("Test Room1", "test1", "For testing!", "It looks boring :(");
            Item s = new Weapon("sword", "swords", "It looks kind of dull...", 5, 10, true, true);

            //TextEngine.Parsing.SymbolTable.Add(r.Name, r);
            SymbolTable.Add(s.Name, s);
            TextEngine.AddRoom(r);

            var src = "add item 'sword' to 'test1'";
            var p = new ScriptParser();
            var pRes = p.Parse(src);
            pRes.Accept(new EvaluationVisitor(p.Diagnostics));

            Assert.IsTrue(r.Inventory.HasItem("sword"));
        }

        [TestMethod]
        public void Evaluate_Character_Should_Pass()
        {
            var src = "character \"Frank\" as npc with health 100 and money 150 and description 'handsome' end end";
            var p = new ScriptParser();
            var r = p.Parse(src);
            r.Accept(new EvaluationVisitor(p.Diagnostics));

            AssertNoDiagnostics(p);
        }

        [TestMethod]
        public void Evaluate_Command_Should_Pass()
        {
            var src = "command 'look'";
            var p = new ScriptParser();
            var r = p.Parse(src);
            r.Accept(new EvaluationVisitor(p.Diagnostics));

            AssertNoDiagnostics(p);
        }

        [TestMethod]
        public void Evaluate_ContainerItem_Should_Pass()
        {
            var src = "item 'chest' with pluralName 'chests' and obtainable false and visible true and description 'you can open it' end end";
            var p = new ScriptParser();
            var r = p.Parse(src);
            r.Accept(new EvaluationVisitor(p.Diagnostics));

            AssertNoDiagnostics(p);
        }

        [TestMethod]
        public void Evaluate_Exit_Should_Pass()
        {
            Room kitchen = new Room("Kitchen", "kitchen");
            Room dining = new Room("Dining", "dining");
            TextEngine.AddRoom(kitchen);
            TextEngine.AddRoom(dining);
            var src = "exit 'DiningRoomE' with fromRoom 'kitchen' and locked false and visible true and side 'north' and toRoom 'DiningRoom' end end";
            var p = new ScriptParser();
            var r = p.Parse(src);
            r.Accept(new EvaluationVisitor(p.Diagnostics));

            AssertNoDiagnostics(p);
        }

        [TestMethod]
        public void Evaluate_Full_Room_Should_Pass()
        {
            string room =
                @"room 'kitchen'
                    with shortName 'kitchen2' and
                        description 'It looks modren' and
                        lookDescription 'Uhh. It smells very tasty'
                    end
                   end";

            var p = new ScriptParser();
            var r = p.Parse(room);
            r.Accept(new EvaluationVisitor(p.Diagnostics));

            Assert.IsTrue(TextEngine.RoomExists("kitchen"));
            AssertNoDiagnostics(p);
        }

        [TestMethod]
        public void Evaluate_Item_Should_Pass()
        {
            var src = "item 'pencil' with pluralName 'pencils' and obtainable true and visible true and description 'you write with it' end end";
            var p = new ScriptParser();
            var r = p.Parse(src);
            r.Accept(new EvaluationVisitor(p.Diagnostics));

            AssertNoDiagnostics(p);
        }

        [TestMethod]
        public void Evaluate_Item_In_Room_Inv_Should_Pass()
        {
            Room room = new Room("Item_In_Room", "In_Room_Inv");
            TextEngine.AddRoom(room);
            var src = "item 'roomPen' with pluralName 'pens' and obtainable true and visible true and description 'you write with it' and location 'In_Room_Inv' end end";
            var p = new ScriptParser();
            var r = p.Parse(src);
            r.Accept(new EvaluationVisitor(p.Diagnostics));
            Assert.IsTrue(TextEngine.GetRoom("In_Room_Inv").Inventory.HasItem("roomPen"));
            AssertNoDiagnostics(p);
        }

        [TestMethod]
        public void Evaluate_Item_In_NPC_Inv_Should_Pass()
        {
            var npc = new NPC("Item_In_NPC");
            TextEngine.AddNPC(npc);
            var src = "item 'NPCPen' with pluralName 'pens' and obtainable true and visible true and description 'you write with it' and location 'Item_In_NPC' end end";
            var p = new ScriptParser();
            var r = p.Parse(src);
            r.Accept(new EvaluationVisitor(p.Diagnostics));
            Assert.IsTrue(TextEngine.GetNPC("Item_In_NPC").Inventory.HasItem("NPCPen"));
            AssertNoDiagnostics(p);
        }

        [TestMethod]
        public void Evaluate_Item_In_ContainerItem_Inv_Should_Pass()
        {
            var ci = new ContainerItem("Item_In_CI", "For testing", true, false, 10);
            SymbolTable.Add(ci.Name, ci);
            var src = "item 'CIPen' with pluralName 'pens' and obtainable true and visible true and description 'you write with it' and location 'Item_In_CI' end end";
            var p = new ScriptParser();
            var r = p.Parse(src);
            r.Accept(new EvaluationVisitor(p.Diagnostics));
            Assert.IsTrue(SymbolTable.GetInstance<ContainerItem>("Item_In_CI").Inventory.HasItem("CIPen"));
            AssertNoDiagnostics(p);
        }

        [TestMethod]
        public void Evaluate_Item_In_Player_Inv_Should_Pass()
        {
            Player pl = new Player("Fred");
            TextEngine.Player = pl;
            var src = "item 'pen' with pluralName 'pens' and obtainable true and visible true and description 'you write with it' and location 'player' end end";
            var p = new ScriptParser();
            var r = p.Parse(src);
            r.Accept(new EvaluationVisitor(p.Diagnostics));
            Assert.IsTrue(TextEngine.Player.Inventory.HasItem("pen"));
            AssertNoDiagnostics(p);
        }

        [TestMethod]
        public void Evaluate_Player_Should_Pass()
        {
            var src = "character \"Bob\" as player with health 100 and money 150 and description 'The Hero' end end";
            var p = new ScriptParser();
            var r = p.Parse(src);
            r.Accept(new EvaluationVisitor(p.Diagnostics));

            AssertNoDiagnostics(p);
        }

        [TestMethod]
        public void Evaluate_Room_Should_Pass()
        {
            string room =
                @"room 'Dining Room'
                    with shortName 'DiningRoom' and
                        lookDescription 'It has a table and four chairs.'
                    end
                   end";

            var p = new ScriptParser();
            var r = p.Parse(room);
            r.Accept(new EvaluationVisitor(p.Diagnostics));

            Assert.IsTrue(TextEngine.RoomExists("DiningRoom"));
            AssertNoDiagnostics(p);
        }

        private void AssertNoDiagnostics(ScriptParser parser)
        {
            if (parser.Diagnostics.Any())
            {
                throw new Exception(parser.Diagnostics.First().ToString());
            }
        }
    }
}