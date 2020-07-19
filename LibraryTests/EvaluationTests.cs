﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TextEngine.Parsing;
using TextEngine.Parsing.Diagnostics;

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
            r.Accept(new EvaluationVisitor(new DiagnosticBag()));
        }

        [TestMethod]
        public void Evaluate_Room_Should_Pass()
        {
            string room =
                @"room 'kitchen'
                    with shortName 'kitchen' and
                        lookDescription 'Uhh. It smells very tasty'
                    end
                   end";

            var p = new ScriptParser();
            var r = p.Parse(room);
            r.Accept(new EvaluationVisitor(new DiagnosticBag()));
        }

        [TestMethod]
        public void Evaluate_Exit_Should_Pass()
        {
            var src = "exit 'DiningRoom' with fromRoom 'kitchen' and locked false and visible true and side 'north' and toRoom 'DiningRoom' end end";
            var p = new ScriptParser();
            var r = p.Parse(src);
            r.Accept(new EvaluationVisitor(new DiagnosticBag()));
        }

        [TestMethod]
        public void Evaluate_Character_Should_Pass()
        {
            var src = "character \"leo\" as npc with health 100 and money 150 and description 'handsome' end end";
            var p = new ScriptParser();
            var r = p.Parse(src);
            r.Accept(new EvaluationVisitor(new DiagnosticBag()));
        }
    }
}
