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
        [ExpectedException(typeof(Exception), "Target 'badTarget' is not a valid Room, NPC or ContainerItem")]
        public void Evaluate_Add_Item_BadTarget_Should_Pass()
        {
            var src = "item 'Book' with pluralName 'Books' and obtainable true and visible true and description 'you can read it' end end add item 'Book' to 'badTarget'";
            var p = new ScriptParser();
            var r = p.Parse(src);

            r.Accept(new EvaluationVisitor(p.Diagnostics));
            AssertNoDiagnostics(p);
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
        public void Evaluate_ItemInPlayerInv_Should_Pass()
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