namespace Geten.TextProcessing.Interfaces
{
	public interface IParser
	{
		IAdjectiveMapping Adjectives { get; }

		bool EnableProfanityFilter { get; set; }

		INounSynonyms Nouns { get; }

		IPrepositionMapping Prepositions { get; }

		IVerbSynonyms Verbs { get; }

		ICommand ParseCommand(string command);
	}
}