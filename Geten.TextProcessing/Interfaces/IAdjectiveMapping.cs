namespace Geten.TextProcessing.Interfaces
{
	public interface IAdjectiveMapping
	{
		void Add(string adjective);

		bool CheckAdjectiveExists(string adjective);
	}
}