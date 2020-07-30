using Geten.Core.Parsers.Script;
using Geten.Core.Parsers.Script.Syntax;
using Geten.Core.Parsing.Diagnostics;
using Geten.Runtime.IO;

namespace GetenCompiler
{
	internal class CompilationVisitor : IScriptVisitor
	{
		private readonly GameBinaryBuilder _binary;
		private readonly DiagnosticBag diagnostics;

		public CompilationVisitor(DiagnosticBag diagnostics)
		{
			this.diagnostics = diagnostics;
			_binary = GameBinaryBuilder.Build();
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
			throw new System.NotImplementedException();
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
			throw new System.NotImplementedException();
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
			throw new System.NotImplementedException();
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
			throw new System.NotImplementedException();
		}

		public void Visit(ExitDefinitionNode node)
		{
			throw new System.NotImplementedException();
		}

		public void Visit(RecipeBookDefinition node)
		{
			throw new System.NotImplementedException();
		}

		public void Visit(RecipeDefinitionNode node)
		{
			throw new System.NotImplementedException();
		}
	}
}