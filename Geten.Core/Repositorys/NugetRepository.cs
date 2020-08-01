using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NuGet.Common;
using NuGet.Packaging;
using NuGet.Packaging.Core;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using NuGet.Versioning;

namespace Geten.Core.Repositorys
{
	public class NugetRepository : IGameRepository
	{
		public async Task DownloadGame(string name, string outputPath)
		{
			var cancellationToken = CancellationToken.None;
			var logger = NullLogger.Instance;

			var repository = Repository.Factory.GetCoreV3("https://api.nuget.org/v3/index.json");

			var cache = new SourceCacheContext();
			var resource = await repository.GetResourceAsync<FindPackageByIdResource>();

			var packageVersion = new NuGetVersion(await GetVersion(name));
			using var packageStream = new MemoryStream();
			var downloader = await resource.GetPackageDownloaderAsync(new PackageIdentity(name, packageVersion), cache, logger, cancellationToken);

			await resource.CopyNupkgToStreamAsync(
				name,
				packageVersion,
				packageStream,
				cache,
				logger,
				CancellationToken.None);

			using var packageReader = new PackageArchiveReader(packageStream);
			var items = (await packageReader.GetContentItemsAsync(CancellationToken.None)).ToArray();
			var gameFile = items.First().Items.First();

			packageReader.ExtractFile(gameFile, Path.Combine(outputPath, Path.GetFileName(gameFile)), logger);
		}

		public async Task<IEnumerable<string>> GetAvailableGames()
		{
			var cancellationToken = CancellationToken.None;
			var logger = NullLogger.Instance;

			var repository = Repository.Factory.GetCoreV3("https://api.nuget.org/v3/index.json");

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

		public async Task<IEnumerable<string>> Search(string query)
		{
			var cancellationToken = CancellationToken.None;
			var logger = NullLogger.Instance;

			var repository = Repository.Factory.GetCoreV3("https://api.nuget.org/v3/index.json");

			var resource = await repository.GetResourceAsync<PackageSearchResource>();
			var searchFilter = new SearchFilter(true);

			var searchResult = await resource.SearchAsync(
				query,
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

		private async Task<string> GetVersion(string name)
		{
			var cancellationToken = CancellationToken.None;
			var logger = NullLogger.Instance;

			var repository = Repository.Factory.GetCoreV3("https://api.nuget.org/v3/index.json");

			var resource = await repository.GetResourceAsync<PackageSearchResource>();
			var searchFilter = new SearchFilter(true);

			var searchResult = await resource.SearchAsync(
				name,
				searchFilter,
				skip: 0,
				take: 20,
				logger,
				cancellationToken);
			var filteredResult = searchResult.FirstOrDefault(_ => _.Tags == "geten");

			return filteredResult.Identity.Version.ToNormalizedString();
		}
	}
}