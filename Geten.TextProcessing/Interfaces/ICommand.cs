using Geten.TextProcessing.Synonyms;

namespace Geten.TextProcessing.Interfaces
{
	public interface ICommand
	{
		string Adjective { get; set; }
		string Adjective2 { get; set; }
		string Adjective3 { get; set; }
		string FullTextCommand { get; set; }

		string Noun { get; set; }
		string Noun2 { get; set; }
		string Noun3 { get; set; }
		PropositionEnum Preposition { get; set; }
		PropositionEnum Preposition2 { get; set; }
		string Profanity { get; set; }
		bool ProfanityDetected { get; set; }
		VerbCodes Verb { get; set; }
	}
}