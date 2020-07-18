using System;
using TextEngine.CommandParsing;

namespace TextEngine.Commands
{
    [CommandName("quit")]
    class QuitCommand : ITextCommand
    {
        public void Invoke()
        {
            // TODO Save gamesate?
            Environment.Exit(0);
        }

        public ITextCommand Parse(CommandParser parser)
        {
            return new QuitCommand();
        }
    }
}
