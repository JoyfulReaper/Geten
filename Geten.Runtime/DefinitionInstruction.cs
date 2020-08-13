using Geten.Core.Parsers.Script.Syntax;

namespace Geten.Runtime
{
	public class DefinitionInstruction : Instruction
	{
		public int EventIndex { get; set; }
		public bool HasEventIndex { get; set; }
		public bool HasProps { get; set; }

		public PropertyList Properties { get; set; }
	}
}