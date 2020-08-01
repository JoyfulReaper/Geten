using System.Collections.Generic;
using System.Threading.Tasks;

namespace Geten.Core
{
	public interface IGameRepository
	{
		public Task DownloadGame(string id);

		public Task<IEnumerable<string>> GetAvailableGames();

		public Task<IEnumerable<string>> Search(string query);
	}
}