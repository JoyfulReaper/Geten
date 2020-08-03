namespace Geten.TextProcessing
{
	/// <summary>
	/// This enumeration helps to control the state machine for the parser.
	/// </summary>
	public enum ParserStatesEnum
	{
		None = 0,
		Verb = 1,
		Noun = 2,
		Preposition = 3,
		Noun2 = 4,
		Preposition2 = 5,
		Noun3 = 6
	}
}