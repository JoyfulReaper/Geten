using Geten.TextProcessing.Synonyms;

namespace Geten.TextProcessing.Interfaces
{
	public interface IPrepositionMapping
	{
		void Add(string inputProposition, PropositionEnum preposition);

		PropositionEnum GetPreposition(string preposition);
	}
}