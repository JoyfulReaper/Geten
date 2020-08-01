using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NuGet.Common;
using NuGet.Configuration;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;

namespace Geten.Core.Repositorys
{
	public class NugetRepository : IGameRepository
	{
		public Task DownloadGame(string id)
		{
			throw new System.NotImplementedException();
		}

		public async Task<IEnumerable<string>> GetAvailableGames()
		{
			var cancellationToken = CancellationToken.None;
			var logger = NullLogger.Instance;

			var cache = new SourceCacheContext();
			var repository = Repository.Factory.GetCoreV3("https://api.nuget.org/v3/index.json");

			var resource = await repository.GetResourceAsync<PackageSearchResource>();
			var searchFilter = new SearchFilter(true);

			var results = await resource.SearchAsync(
				"geten",
				searchFilter,
				skip: 0,
				take: 20,
				logger,
				cancellationToken);

			return new[] { "" };
		}
	}
}