using System;
using System.Linq;
using Geten.Core;
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
			var result = repo.GetAvailableGames().Result.ToArray();

			repo.DownloadGame(result[0], Environment.CurrentDirectory).Wait();
		}

		[TestMethod]
		public void Install_Game_Should_Pass()
		{
			GameRepository.InstallGameAsync("TestGame", new NugetRepository()).Wait();

			Assert.IsTrue(GameRepository.IsInstalled("TestGame"));
		}
	}
}