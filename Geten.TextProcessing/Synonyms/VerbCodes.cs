namespace Geten.TextProcessing.Synonyms
{
	/// <summary>
	/// Supported base verbs used by the game engine. You can have many different synonyms mapped to the verbs, but the
	/// verb synonyms will map to one of these codes from the parser.
	/// </summary>
	public enum VerbCodes
	{
		NoCommand = 0,
		Go = 1,
		Take = 2,
		Use = 3,
		Look = 4,
		Drop = 5,
		Hint = 6,
		Attack = 7,
		Visit = 8,
		Eat = 9
	}
}