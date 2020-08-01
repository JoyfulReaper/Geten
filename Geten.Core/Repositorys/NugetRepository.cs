using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NuGet.Common;
using NuGet.Packaging;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using NuGet.Versioning;

namespace Geten.Core.Repositorys
{
	public class NugetRepository : IGameRepository
	{
		private CancellationToken cancellationToken;
		private ILogger logger;
		private SourceRepository repository;

		public NugetRepository()
		{
			cancellationToken = CancellationToken.None;
			logger = NullLogger.Instance;

			repository = Repository.Factory.GetCoreV3("https://api.nuget.org/v3/index.json");
		}

		public async Task DownloadGame(string id)
		{
			var spl = id.Split(':');

			var cache = new SourceCacheContext();
			var resource = await repository.GetResourceAsync<FindPackageByIdResource>();

			var packageId = spl[0];
			var packageVersion = new NuGetVersion(spl[1]);
			using var packageStream = new MemoryStream();

			await resource.CopyNupkgToStreamAsync(
				packageId,
				packageVersion,
				packageStream,
				cache,
				logger,
				cancellationToken);

			using var packageReader = new PackageArchiveReader(packageStream);
			var nuspecReader = await packageReader.GetNuspecReaderAsync(cancellationToken);

			var files = nuspecReader.GetContentFiles().ToArray();
		}

		public async Task<IEnumerable<string>> GetAvailableGames()
		{
			var resource = await repository.GetResourceAsync<PackageSearchResource>();
			var searchFilter = new SearchFilter(true);

			var searchResult = await resource.SearchAsync(
				"geten",
				searchFilter,
				skip: 0,
				take: 20,
				logger,
				cancellationToken);
			var filteredResult = searchResult.Where(_ => _.Tags == "geten");

			var result = new List<string>();
			foreach (var p in filteredResult)
			{
				result.Add(p.Identity.Id + ":" + p.Identity.Version.ToNormalizedString());
			}
			return result;
		}
	}
}