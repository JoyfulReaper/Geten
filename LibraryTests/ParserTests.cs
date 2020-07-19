using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using TextEngine.Parsing;

namespace LibraryTests
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void Parse_Include_Should_Pass()
        {
            var src = "include \"base.script\"";
            var parser = new ScriptParser();
            var result = parser.Parse(src);

            AssertNoDiagnostics(parser);
        }

        private void AssertNoDiagnostics(ScriptParser parser)
        {
            if(parser.Diagnostics.Any())
            {
                throw new Exception(parser.Diagnostics.First().ToString());
            }
        }

        [TestMethod]
        public void Parse_Character_Should_Pass()
        {
            var src = "character \"leo\" with health 100 and money 150 end end";
            var parser = new ScriptParser();
            var result = parser.Parse(src);

            AssertNoDiagnostics(parser);
        }

        [TestMethod]
        public void Parse_Character_Event_Should_Pass()
        {
            var src = 
            @"character ""leo"" 
                with 
                    health 100 and 
                    money 150 
                end 
                on ""move"" 
                    tell ""blub"" 
                end 
            end";
            var parser = new ScriptParser();
            var result = parser.Parse(src);

            AssertNoDiagnostics(parser);
        }

        [TestMethod]
        public void Parse_Weapon_Should_Pass()
        {
            var src = "weapon \"sword\" with mindamage 10 and maxdamage 35 end end";
            var parser = new ScriptParser();
            var result = parser.Parse(src);

            AssertNoDiagnostics(parser);
        }

        [TestMethod]
        public void Parse_Many_Should_Pass()
        {
            var src = "include \"base.script\"\nkey \"blub\" with maxusage 10 end end";
            var parser = new ScriptParser();
            var result = parser.Parse(src);

            AssertNoDiagnostics(parser);
        }

        [TestMethod]
        public void Parse_Memory_Should_Pass()
        {
            var src = "memory \"name\" equals \"dodo\"";
            var parser = new ScriptParser();
            var result = parser.Parse(src);

            AssertNoDiagnostics(parser);
        }

        [TestMethod]
        public void Parse_Memory_Empty_Should_Pass()
        {
            var src = "memory \"name\"";
            var parser = new ScriptParser();
            var result = parser.Parse(src);

            AssertNoDiagnostics(parser);
        }

        [TestMethod]
        public void Parse_EventSubscription_Should_Pass()
        {
            var src = "on \"start\" tell \"hello world\" end";
            var parser = new ScriptParser();
            var result = parser.Parse(src);

            AssertNoDiagnostics(parser);
        }

        [TestMethod]
        public void Parse_AskFor_Should_Pass()
        {
            var src = "ask for \"name?\" to \"name\"";
            var parser = new ScriptParser();
            var result = parser.Parse(src);

            AssertNoDiagnostics(parser);
        }

        [TestMethod]
        public void Parse_Command_Should_Pass()
        {
            var src = "command 'look'";
            var parser = new ScriptParser();
            var result = parser.Parse(src);

            AssertNoDiagnostics(parser);
        }

        [TestMethod]
        public void Parse_Room_Should_Pass()
        {
            var src = "room \"kitchen\" with shortName \"kitchen\" and lookDescription \"Uhh. It smells very tasty\" end end";
            var parser = new ScriptParser();
            var result = parser.Parse(src);

            AssertNoDiagnostics(parser);
        }

        [TestMethod]
        public void Parse_Exit_Should_Pass()
        {
            var src = "exit 'DiningRoom' with fromRoom 'kitchen' and locked false and visible true and side 'north' and toRoom 'DiningRoom' end end";
            var parser = new ScriptParser();
            var result = parser.Parse(src);

            AssertNoDiagnostics(parser);
        }

        [TestMethod]
        public void Parse_Increase_Should_Pass()
        {
            var src = "increase health of \"sarah\" by 15";
            var parser = new ScriptParser();
            var result = parser.Parse(src);

            AssertNoDiagnostics(parser);
        }

        [TestMethod]
        public void Parse_Decrease_Should_Pass()
        {
            var src = "decrease health of \"sarah\" by 15";
            var parser = new ScriptParser();
            var result = parser.Parse(src);

            AssertNoDiagnostics(parser);
        }

        [TestMethod]
        public void Parse_Item_With_Boolean_Should_Pass()
        {
            var src = "item 'hello' with isLocked true end end";
            var parser = new ScriptParser();
            var result = parser.Parse(src);

            AssertNoDiagnostics(parser);
        }

        [TestMethod]
        public void Parse_setProperty_No_Target_Should_Pass()
        {
            var src = "setProperty isLocked false";
            var parser = new ScriptParser();
            var result = parser.Parse(src);

            AssertNoDiagnostics(parser);
        }

        [TestMethod]
        public void Parse_setProperty_with_Target_Should_Pass()
        {
            var src = "setProperty of \"Chest\" isLocked false";
            var parser = new ScriptParser();
            var result = parser.Parse(src);

            AssertNoDiagnostics(parser);
        }

        [TestMethod]
        public void Parse_Remove_Item_Should_Pass()
        {
            var src = "remove item \"apple\"";
            var parser = new ScriptParser();
            var result = parser.Parse(src);

            AssertNoDiagnostics(parser);
        }

        [TestMethod]
        public void Parse_Remove_Item_With_Target_Should_Pass()
        {
            var src = "remove item \"apple\" from \"chest\"";
            var parser = new ScriptParser();
            var result = parser.Parse(src);

            AssertNoDiagnostics(parser);
        }

        [TestMethod]
        public void Parse_Add_Item_Should_Pass()
        {
            var src = "add item \"apple\"";
            var parser = new ScriptParser();
            var result = parser.Parse(src);

            AssertNoDiagnostics(parser);
        }

        [TestMethod]
        public void Parse_Add_Item_With_Target_Should_Pass()
        {
            var src = "add item \"apple\" to \"chest\"";
            var parser = new ScriptParser();
            var result = parser.Parse(src);

            AssertNoDiagnostics(parser);
        }

        [TestMethod]
        public void Parse_Dialog_Should_Pass()
        {
            var src = "dialog \"apple_Monolog\"";
            var parser = new ScriptParser();
            var result = parser.Parse(src);

            AssertNoDiagnostics(parser);
        }

        [TestMethod]
        public void Parse_Play_Should_Pass()
        {
            var src = "play \"something.wav\"";
            var parser = new ScriptParser();
            var result = parser.Parse(src);

            AssertNoDiagnostics(parser);
        }

        [TestMethod]
        public void Parse_Play_in_loop_Should_Pass()
        {
            var src = "play \"something.wav\" in loop";
            var parser = new ScriptParser();
            var result = parser.Parse(src);

            AssertNoDiagnostics(parser);
        }
    }
}