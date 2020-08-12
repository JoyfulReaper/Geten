﻿using Geten.TextProcessing;
using Geten.TextProcessing.Interfaces;
using Geten.TextProcessing.Synonyms;

namespace Geten.Core.Commands
{
	[CommandHandler(VerbCodes.Go)]
	internal class GoCommand : ICommandHandler, ITextCommand
	{
		public GoCommand()
		{
		}

		public void Invoke()
		{
		}

		public void Invoke(Command cmd)
		{
			var dir = TextEngine.GetDirectionFromString(cmd.Noun);

			if (dir == Direction.Invalid)
			{
				TextEngine.AddMessage($"You don't know how to go {cmd.Noun}");
				return;
			}

			var location = TextEngine.Player.Location.GetSide(dir);
			location.Enter(TextEngine.Player, dir);
		}
	}
}