using System.Collections.ObjectModel;

namespace Geten.TextProcessing.Interfaces
{
	public interface IProfanityFilter
	{
		ReadOnlyCollection<string> DetectAllProfanities(string sentence);

		bool IsProfanity(string word);

		string StringContainsFirstProfanity(string sentence);
	}
}