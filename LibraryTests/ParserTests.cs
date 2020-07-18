using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using TextEngine.Parsing;

namespace LibraryTests
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void Parse_Character_Should_Pass()
        {
            var src = "character \"leo\" with health 100 and money 150 end";
            var parser = new ScriptParser();
            var result = parser.Parse(src);
        }

        [TestMethod]
        public void Parse_Character_Event_Should_Pass()
        {
            var src = "character \"leo\" with health 100 and money 150 end on \"move\" tell \"blub\" end end";
            var parser = new ScriptParser();
            var result = parser.Parse(src);
        }

        [TestMethod]
        public void Parse_Weapon_Should_Pass()
        {
            var src = "weapon \"sword\" with mindamage 10 and maxdamage 35 end";
            var parser = new ScriptParser();
            var result = parser.Parse(src);
        }

        [TestMethod]
        public void Parse_Many_Should_Pass()
        {
            var src = "include \"base.script\"\nkey \"blub\" with maxusage 10 end end\nweapon \"sword\" with mindamage 10 and maxdamage 35 end character \"leo\" with health 100 and money 150 end end";
            var parser = new ScriptParser();
            var result = parser.Parse(src);
        }

        [TestMethod]
        public void Parse_Memory_Should_Pass()
        {
            var src = "memory \"name\" equals \"dodo\"";
            var parser = new ScriptParser();
            var result = parser.Parse(src);
        }

        [TestMethod]
        public void Parse_Memory_Empty_Should_Pass()
        {
            var src = "memory \"name\"";
            var parser = new ScriptParser();
            var result = parser.Parse(src);
        }

        [TestMethod]
        public void Parse_EventSubscription_Should_Pass()
        {
            var src = "on \"start\" tell \"hello world\" end";
            var parser = new ScriptParser();
            var result = parser.Parse(src);
        }

        [TestMethod]
        public void Parse_AskFor_Should_Pass()
        {
            var src = "ask for \"name?\" to \"name\"";
            var parser = new ScriptParser();
            var result = parser.Parse(src);
        }

        [TestMethod]
        public void Parse_Command_Should_Pass()
        {
            var src = "command 'look'";
            var parser = new ScriptParser();
            var result = parser.Parse(src);
        }

        [TestMethod]
        public void Parse_Room_Should_Pass()
        {
            var src = "room \"kitchen\" with shortName \"kitchen\" and lookDescription \"Uhh. It smells very tasty\" end";
            var parser = new ScriptParser();
            var result = parser.Parse(src);
        }

        [TestMethod]
        public void Parse_Increase_Should_Pass()
        {
            var src = "increase health of \"sarah\" by 15";
            var parser = new ScriptParser();
            var result = parser.Parse(src);
        }

        [TestMethod]
        public void Parse_Item_With_Boolean_Should_Pass()
        {
            var src = "item 'hello' with isLocked true end";
            var parser = new ScriptParser();
            var result = parser.Parse(src);
        }
    }
}