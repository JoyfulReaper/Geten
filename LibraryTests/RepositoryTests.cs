using Geten.Core.Repositorys;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibraryTests
{
	[TestClass]
	public class RepositoryTests
	{
		[TestMethod]
		public void GetPackages_Should_Pass()
		{
			var repo = new NugetRepository();
			var result = repo.GetAvailableGames().Result;
		}
	}
}