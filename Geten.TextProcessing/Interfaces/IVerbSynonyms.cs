using Geten.TextProcessing.Synonyms;

namespace Geten.TextProcessing.Interfaces
{
	public interface IVerbSynonyms
	{
		void Add(string synonym, VerbCodes verb);

		VerbCodes GetVerbForSynonym(string synonym);
	}
}