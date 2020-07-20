﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using TextEngine;
using TextEngine.Parsing;

namespace LibraryTests
{
    [TestClass]
    public class EvaluationTests
    {
        [TestMethod]
        public void Evaluate_Full_Room_Should_Pass()
        {
            string room =
                @"room 'kitchen'
                    with shortName 'kitchen' and
                        description 'It looks modren' and
                        lookDescription 'Uhh. It smells very tasty'
                    end
                   end";

            var p = new ScriptParser();
            var r = p.Parse(room);
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

            AssertNoDiagnostics(p);
        }

        [TestMethod]
        public void Evaluate_Exit_Should_Pass()
        {
            var src = "exit 'DiningRoom' with fromRoom 'kitchen' and locked false and visible true and side 'north' and toRoom 'DiningRoom' end end";
            var p = new ScriptParser();
            var r = p.Parse(src);
            r.Accept(new EvaluationVisitor(p.Diagnostics));

            AssertNoDiagnostics(p);
        }

        [TestMethod]
        public void Evaluate_Character_Should_Pass()
        {
            var src = "character \"leo\" as npc with health 100 and money 150 and description 'handsome' end end";
            var p = new ScriptParser();
            var r = p.Parse(src);
            r.Accept(new EvaluationVisitor(p.Diagnostics));

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
        public void Evaluate_ContainerItem_Should_Pass()
        {
            var src = "item 'chest' with pluralName 'chests' and obtainable false and visible true and description 'you can open it' end end";
            var p = new ScriptParser();
            var r = p.Parse(src);
            r.Accept(new EvaluationVisitor(p.Diagnostics));

            AssertNoDiagnostics(p);
        }

        [TestMethod]
        public void Evaluate_ItemInPlayerInv_Should_Pass()
        {
            Player pl = new Player("Fred");
            TextEngine.TextEngine.Player = pl;
            var src = "item 'pen' with pluralName 'pens' and obtainable true and visible true and description 'you write with it' and location 'player' end end";
            var p = new ScriptParser();
            var r = p.Parse(src);
            r.Accept(new EvaluationVisitor(p.Diagnostics));
            Assert.IsTrue(TextEngine.TextEngine.Player.Inventory.HasItem("pen"));
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

        private void AssertNoDiagnostics(ScriptParser parser)
        {
            if (parser.Diagnostics.Any())
            {
                throw new Exception(parser.Diagnostics.First().ToString());
            }
        }
    }
}
