using System;
using Geten.TextProcessing;
using Geten.TextProcessing.Interfaces;
using Geten.TextProcessing.Synonyms;

namespace Geten.Core.Commands
{
	[CommandHandler(VerbCodes.Quit)]
	internal class QuitCommand : ICommandHandler, ITextCommand
	{
		public void Invoke()
		{
			// TODO Save gamestate?
			Environment.Exit(0);
		}

		public void Invoke(Command cmd)
		{
			Environment.Exit(0);
		}
	}
}