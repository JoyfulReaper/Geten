using System;
using TextEngine.CommandParsing;

namespace TextEngine.Commands
{
    [CommandName("quit")]
    class QuitCommand : ITextCommand
    {
        public void Invoke()
        {
            // TODO Save gamestate?
            Environment.Exit(0);
        }
    }
}
