using System;

namespace Geten.Core.Commands
{
    internal class QuitCommand : ITextCommand
    {
        public void Invoke()
        {
            // TODO Save gamestate?
            Environment.Exit(0);
        }
    }
}