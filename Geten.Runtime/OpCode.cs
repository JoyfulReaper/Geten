namespace Geten.Runtime
{
	public enum OpCode
	{
		NOP = 0,

		CharackterDefinition = 1,
		NPCDefinition = 2,
		ItemDefinition = 3,
		RoomDefinition = 4,
		ExitDefinition = 5,
		WeaponDefinition = 6,
		KeyDefinition = 7,
		RecipeBookDefinition = 8,
		RecipeDefinition = 9,

		Tell = 15,
		AskForInput = 16,
		MemorySlotAllocation = 20,
	}
}