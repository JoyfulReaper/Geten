﻿using Geten.Core;
using Geten.Core.Factories;
using Geten.Core.GameObjects;
using Geten.Core.MapItems;
using Geten.Core.Parsers.Script;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace LibraryTests
{
	[TestClass]
	public class EvaluationTests
	{
		[TestMethod]
		public void Evaluate_Add_Item_BadTarget_Should_Pass()
		{
			var src = "item 'Book' with pluralName 'Books' and obtainable true and visible true and description 'you can read it' end end add item 'Book' to 'badTarget'";
			var p = new ScriptParser();
			var r = p.Parse(src);
			r.Accept(visitor: new EvaluationVisitor(p.Diagnostics));
			Assert.AreEqual(p.Diagnostics.First().ToString(), "Target 'badTarget' is not a valid Room, NPC or ContainerItem");
		}

		[TestMethod]
		public void Evaluate_Add_Item_to_ContainerItem_Should_Pass()
		{
			// ContainerItem ci = GameObject.Create<ContainerItem>("Brown Bag", "It has a cookie in it", true, true);
			var ci = GameObject.Create<ContainerItem>("Brown Bag");
			var src = "item 'cookie' with description 'chocolate chip' end end add item 'cookie' to 'Brown Bag'";

			var p = new ScriptParser();
			var res = p.Parse(src);
			res.Accept(new EvaluationVisitor(p.Diagnostics));

			Assert.IsTrue(ci.Inventory.HasItem("cookie"));

			AssertNoDiagnostics(p);
		}

		[TestMethod]
		public void Evaluate_Add_Item_to_NPC_Should_Pass()
		{
			GameObject.Create<NPC>("Some Guy");
			GameObject.Create<Item>("Bag of Chips");

			var src = "add item 'Bag of Chips' to 'Some Guy'";
			var p = new ScriptParser();
			var res = p.Parse(src);
			res.Accept(new EvaluationVisitor(p.Diagnostics));

			Assert.IsTrue(SymbolTable.GetInstance<NPC>("Some Guy").Inventory.HasItem("Bag of Chips"));

			AssertNoDiagnostics(p);
		}

		[TestMethod]
		public void Evaluate_Add_Item_to_Room_Should_Pass()
		{
			GameObject.Create<Room>("Test Room1");
			GameObject.Create<Weapon>("sword");

			var src = "add item 'sword' to 'Test Room1'";
			var p = new ScriptParser();
			var pRes = p.Parse(src);
			pRes.Accept(new EvaluationVisitor(p.Diagnostics));

			Assert.IsTrue(SymbolTable.GetInstance<Room>("Test Room1").Inventory.HasItem("sword"));

			AssertNoDiagnostics(p);
		}

		[TestMethod]
		public void Evaluate_Character_Should_Pass()
		{
			var src = "character \"Frank\" as npc with health 25 and money 150 and description 'handsome' end end";
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
		public void Evaluate_Decrease_Bad_NPC_Health_Should_Pass()
		{
			var src = "decrease health of 'NOTestNPC' by 10";
			var p = new ScriptParser();
			var r = p.Parse(src);
			try
			{
				r.Accept(new EvaluationVisitor(p.Diagnostics));
				AssertNoDiagnostics(p);
			}
			catch (Exception e)
			{
				Assert.AreEqual(e.Message, "'notestnpc' is not declared");
			}
		}

		[TestMethod]
		public void Evaluate_Decrease_NPC_Health_Should_Pass()
		{
			var npc = GameObject.Create<NPC>("TestNPC2");

			var src = "decrease health of 'TestNPC2' by 10";
			var p = new ScriptParser();
			var r = p.Parse(src);

			r.Accept(new EvaluationVisitor(p.Diagnostics));
			AssertNoDiagnostics(p);
			Assert.IsTrue(SymbolTable.GetInstance<NPC>("TestNPC2").Health == 90);
		}

		[TestMethod]
		public void Evaluate_Decrease_Player_Health_Should_Pass()
		{
			//var player = GameObject.Create<Player>("TestPlayer2", "He likes to test!", 90, 100);
			var player = GameObject.Create<Player>("TestPlayer2");
			TextEngine.Player = player;

			var src = "decrease health of 'player' by 10";
			var p = new ScriptParser();
			var r = p.Parse(src);

			r.Accept(new EvaluationVisitor(p.Diagnostics));
			AssertNoDiagnostics(p);
			Assert.IsTrue(TextEngine.Player.Health == 90);
		}

		[TestMethod]
		public void Evaluate_Exit_Should_Pass()
		{
			var kitchen = GameObject.Create<Room>("Kitchen");
			var dining = GameObject.Create<Room>("Dining Room");
			var src = "exit 'DiningRoomE' with fromRoom 'kitchen' and locked false and visible true and side 'north' and toRoom 'Dining Room' end end";
			var p = new ScriptParser();
			var r = p.Parse(src);
			r.Accept(new EvaluationVisitor(p.Diagnostics));

			AssertNoDiagnostics(p);
		}

		[TestMethod]
		public void Evaluate_Full_Room_Should_Pass()
		{
			var room =
				@"room 'test kitchen'
					with
						description 'It looks modren' and
						lookDescription 'Uhh. It smells very tasty'
					end
				   end";

			var p = new ScriptParser();
			var r = p.Parse(room);
			r.Accept(new EvaluationVisitor(p.Diagnostics));

			Assert.IsTrue(SymbolTable.Contains("test kitchen"));
			AssertNoDiagnostics(p);
		}

		[TestMethod]
		public void Evaluate_Increase_NPC_Health_Should_Pass()
		{
			var npc = GameObject.Create<NPC>("TestNPC1");

			var src = "increase health of 'TestNPC1' by 10";
			var p = new ScriptParser();
			var r = p.Parse(src);

			r.Accept(new EvaluationVisitor(p.Diagnostics));
			AssertNoDiagnostics(p);
			Assert.IsTrue(SymbolTable.GetInstance<NPC>("TestNPC1").Health == 100);
		}

		[TestMethod]
		public void Evaluate_Increase_Player_Health_Should_Pass()
		{
			var player = GameObject.Create<Player>("TestPlayer1");
			player.Health = 90;
			TextEngine.Player = player;

			var src = "increase health of 'player' by 10";
			var p = new ScriptParser();
			var r = p.Parse(src);

			r.Accept(new EvaluationVisitor(p.Diagnostics));
			AssertNoDiagnostics(p);
			Assert.IsTrue(TextEngine.Player.Health == 100);
		}

		[TestMethod]
		public void Evaluate_Item_In_ContainerItem_Inv_Should_Pass()
		{
			var ci = GameObject.Create<ContainerItem>("Item_In_CI");
			var src = "item 'CIPen' with pluralName 'pens' and obtainable true and visible true and description 'you write with it' and location 'Item_In_CI' end end";
			var p = new ScriptParser();
			var r = p.Parse(src);
			r.Accept(new EvaluationVisitor(p.Diagnostics));
			Assert.IsTrue(SymbolTable.GetInstance<ContainerItem>("Item_In_CI").Inventory.HasItem("CIPen"));
			AssertNoDiagnostics(p);
		}

		[TestMethod]
		public void Evaluate_Item_In_NPC_Inv_Should_Pass()
		{
			var npc = GameObject.Create<NPC>("Item_In_NPC");
			var src = "item 'NPCPen' with pluralName 'pens' and obtainable true and visible true and description 'you write with it' and location 'Item_In_NPC' end end";
			var p = new ScriptParser();
			var r = p.Parse(src);
			r.Accept(new EvaluationVisitor(p.Diagnostics));
			Assert.IsTrue(SymbolTable.GetInstance<NPC>("Item_In_NPC").Inventory.HasItem("NPCPen"));
			AssertNoDiagnostics(p);
		}

		[TestMethod]
		public void Evaluate_Item_In_Player_Inv_Should_Pass()
		{
			var pl = GameObject.Create<Player>("Fred");
			TextEngine.Player = pl;
			var src = "item 'pen' with pluralName 'pens' and obtainable true and visible true and description 'you write with it' and location 'player' end end";
			var p = new ScriptParser();
			var r = p.Parse(src);
			r.Accept(new EvaluationVisitor(p.Diagnostics));
			Assert.IsTrue(TextEngine.Player.Inventory.HasItem("pen"));
			AssertNoDiagnostics(p);
		}

		[TestMethod]
		public void Evaluate_Item_In_Room_Inv_Should_Pass()
		{
			var room = GameObject.Create<Room>("Item_In_Room");
			var src = "item 'roomPen' with pluralName 'pens' and obtainable true and visible true and description 'you write with it' and location 'Item_In_Room' end end";
			var p = new ScriptParser();
			var r = p.Parse(src);
			r.Accept(new EvaluationVisitor(p.Diagnostics));
			Assert.IsTrue(SymbolTable.GetInstance<Room>("Item_In_Room").Inventory.HasItem("roomPen"));
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
		public void Evaluate_Player_Should_Pass()
		{
			var src = "character \"Bob\" as player with health 100 and money 150 and description 'The Hero' end end";
			var p = new ScriptParser();
			var r = p.Parse(src);
			r.Accept(new EvaluationVisitor(p.Diagnostics));

			AssertNoDiagnostics(p);
		}

		[TestMethod]
		public void Evaluate_Remove_Item_BadTarget_Should_Pass()
		{
			var src = "item 'remBook' with pluralName 'Books' and obtainable true and visible true and description 'you can read it' end end add item 'remBook' to 'badTarget'";
			var p = new ScriptParser();
			var r = p.Parse(src);
			try
			{
				r.Accept(new EvaluationVisitor(p.Diagnostics));
				AssertNoDiagnostics(p);
			}
			catch (Exception e)
			{
				Assert.AreEqual(e.Message, "Target 'badTarget' is not a valid Room, NPC or ContainerItem");
			}
		}

		[TestMethod]
		public void Evaluate_Remove_Item_From_ContainerItem_Should_Pass()
		{
			var ci = GameObject.Create<ContainerItem>("Empty Bag");
			var src = "item 'cookie2' with description 'chocolate chip' end end add item 'cookie2' to 'Empty Bag'";

			var p = new ScriptParser();
			var res = p.Parse(src);
			res.Accept(new EvaluationVisitor(p.Diagnostics));

			Assert.IsTrue(ci.Inventory.HasItem("cookie2"));

			p = new ScriptParser();
			res = p.Parse("remove item 'cookie2' from 'Empty Bag'");
			res.Accept(new EvaluationVisitor(p.Diagnostics));

			Assert.IsFalse(ci.Inventory.HasItem("cookie2"));

			AssertNoDiagnostics(p);
		}

		[TestMethod]
		public void Evaluate_Remove_Item_From_NPC_Should_Pass()
		{
			var npc = GameObject.Create<NPC>("Remove Guy");
			var item = GameObject.Create<Item>("Empty Bag of Chips");
			var src = "add item 'Empty Bag of Chips' to 'Remove Guy'";
			var p = new ScriptParser();
			var res = p.Parse(src);
			res.Accept(new EvaluationVisitor(p.Diagnostics));

			Assert.IsTrue(npc.Inventory.HasItem("Empty Bag of Chips"));

			src = "remove item 'Empty Bag of Chips' from 'Remove Guy'";
			p = new ScriptParser();
			res = p.Parse(src);
			res.Accept(new EvaluationVisitor(p.Diagnostics));

			Assert.IsFalse(npc.Inventory.HasItem("Empty Bag of Chips"));

			AssertNoDiagnostics(p);
		}

		[TestMethod]
		public void Evaluate_Remove_Item_From_Room_Should_Pass()
		{
			var r = GameObject.Create<Room>("Test Room2");
			Item s = GameObject.Create<Weapon>("toothpick");

			var src = "add item 'toothpick' to 'Test Room2'";
			var p = new ScriptParser();
			var pRes = p.Parse(src);
			pRes.Accept(new EvaluationVisitor(p.Diagnostics));

			Assert.IsTrue(r.Inventory.HasItem("toothpick"));

			src = "remove item 'toothpick' from 'Test Room2'";
			p = new ScriptParser();
			pRes = p.Parse(src);
			pRes.Accept(new EvaluationVisitor(p.Diagnostics));

			Assert.IsFalse(r.Inventory.HasItem("toothpick"));

			AssertNoDiagnostics(p);
		}

		[TestMethod]
		public void Evaluate_Room_Should_Pass()
		{
			var room =
				@"room 'Test Dining Room'
					with
						lookDescription 'It has a table and four chairs.'
					end
				   end";

			var p = new ScriptParser();
			var r = p.Parse(room);
			r.Accept(new EvaluationVisitor(p.Diagnostics));

			Assert.IsTrue(SymbolTable.Contains("Test Dining Room"));
			AssertNoDiagnostics(p);
		}

		[TestInitialize]
		public void Init()
		{
			if (!ObjectFactory.IsRegisteredFor<GameObject>())
			{
				ObjectFactory.Register<GameObjectFactory, GameObject>();
			}
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