using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CrossPlattformUtils;
using Geten.Core.OSSpecific;

namespace Geten.Core
{
	public static class GameRepository
	{
		public static IEnumerable<string> GetInstalledGames()
		{
			var path = Allocator.New<IDefaultPaths>().GameDirectory; //get path based on running os

			if (Directory.Exists(path))
			{
				return Directory.EnumerateFiles(path, "*.*").Where(_ => Path.GetExtension(_) == "script" || Path.GetExtension(_) == "bingame");
			}
			else
			{
				Directory.CreateDirectory(path);
			}

			return Array.Empty<string>();
		}

		public static async void InstallGame(string name, IGameRepository repo)
		{
			var path = Allocator.New<IDefaultPaths>().GameDirectory;

			await repo.DownloadGame(name, Path.Combine(path, name));
		}

		public static bool IsInstalled(string name)
		{
			var path = Allocator.New<IDefaultPaths>().GameDirectory;

			return File.Exists(Path.Combine(path, name));
		}
	}
}