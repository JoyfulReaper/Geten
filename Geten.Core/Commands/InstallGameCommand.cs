using Geten.Core.MapItems;
using Geten.Core.Repositorys;

namespace Geten.Core.Commands
{
	internal class InstallGameCommand : ITextCommand
	{
		public InstallGameCommand(string game)
		{
			Game = game;
		}

		public string Game { get; }

		public async void Invoke()
		{
			await GameRepository.InstallGameAsync(Game, new NugetRepository());
		}
	}
}