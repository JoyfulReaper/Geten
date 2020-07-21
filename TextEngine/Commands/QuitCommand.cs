using System;

namespace Geten.Commands
{
    class QuitCommand : ITextCommand
    {
        public void Invoke()
        {
            // TODO Save gamestate?
            Environment.Exit(0);
        }
    }
}
