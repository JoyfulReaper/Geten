namespace Geten.Runtime
{
	public class Instruction
	{
		public OpCode OpCode { get; private set; }

		public bool IsDefinition => OpCode.IsDefinition();

		public static Instruction Create(OpCode op)
		{
			return new Instruction { OpCode = op };
		}

		public static Instruction Create(OpCode op, object argument)
		{
			return new OneArgumentInstruction { OpCode = op, Argument = argument };
		}
	}
}