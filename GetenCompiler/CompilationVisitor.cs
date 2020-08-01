using Geten.Core;
using Geten.Core.Crafting;
using Geten.Core.GameObjects;
using Geten.Core.MapItems;
using Geten.Core.Parsers.Script;
using Geten.Core.Parsers.Script.Syntax;
using Geten.Core.Parsing.Diagnostics;
using Geten.Runtime.IO;
using Geten.Runtime.Tables;

namespace GetenCompiler
{
	internal class CompilationVisitor : IScriptVisitor
	{
		private readonly GameBinaryBuilder _binaryBuilder;
		private readonly DiagnosticBag diagnostics;

		private GameObjectTable _objects;

		public CompilationVisitor(DiagnosticBag diagnostics)
		{
			this.diagnostics = diagnostics;
			_binaryBuilder = GameBinaryBuilder.Build();
			_objects = new GameObjectTable();
		}

		public BinaryGameDefinitionFile GetBinary()
		{
			return _binaryBuilder.GetFile();
		}

		public void Visit(BlockNode block)
		{
			foreach (var n in block)
			{
				n.Accept(this);
			}
		}

		public void Visit(AddItemNode node)
		{
			throw new System.NotImplementedException();
		}

		public void Visit(CharacterDefinitionNode node)
		{
			AddGameObjectToTable<Character>(node);
		}

		public void Visit(AskForInputNode node)
		{
			throw new System.NotImplementedException();
		}

		public void Visit(CommandNode node)
		{
			throw new System.NotImplementedException();
		}

		public void Visit(RoutineDefinitionNode node)
		{
			throw new System.NotImplementedException();
		}

		public void Visit(DecreaseNode node)
		{
			throw new System.NotImplementedException();
		}

		public void Visit(IncreaseNode node)
		{
			throw new System.NotImplementedException();
		}

		public void Visit(DialogCallNode node)
		{
			throw new System.NotImplementedException();
		}

		public void Visit(EventSubscriptionNode node)
		{
			throw new System.NotImplementedException();
		}

		public void Visit(ItemDefinitionNode node)
		{
			AddGameObjectToTable<Item>(node);
		}

		public void Visit(KeyDefinitionNode node)
		{
			throw new System.NotImplementedException();
		}

		public void Visit(LiteralNode node)
		{
			throw new System.NotImplementedException();
		}

		public void Visit(MemorySlotDefinition node)
		{
			throw new System.NotImplementedException();
		}

		public void Visit(PlayNode node)
		{
			throw new System.NotImplementedException();
		}

		public void Visit(RemoveItemNode node)
		{
			throw new System.NotImplementedException();
		}

		public void Visit(RoomDefinitionNode node)
		{
			AddGameObjectToTable<Room>(node);
		}

		public void Visit(SetPropertyNode node)
		{
			throw new System.NotImplementedException();
		}

		public void Visit(TellNode node)
		{
			throw new System.NotImplementedException();
		}

		public void Visit(WeaponDefinitionNode node)
		{
			AddGameObjectToTable<Weapon>(node);
		}

		public void Visit(ExitDefinitionNode node)
		{
			AddGameObjectToTable<Exit>(node);
		}

		public void Visit(RecipeBookDefinition node)
		{
			AddGameObjectToTable<RecipeBook>(node);
		}

		public void Visit(RecipeDefinitionNode node)
		{
		}

		private void AddGameObjectToTable<T>(dynamic node)
			where T : GameObject
		{
			var obj = GameObject.Create<Character>(node.NameToken.Text, node.Properties);

			_objects.Add(obj);
		}
	}
}