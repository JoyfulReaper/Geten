namespace Geten.Runtime
{
	public static class Extensions
	{
		public static bool IsDefinition(this OpCode op)
		{
			return op != OpCode.NOP && (byte)op < 10;
		}
	}
}