﻿using Geten.Core.Commands;

namespace Geten.Core.Commands
{
    internal class LookCommand : ITextCommand
    {
        public LookCommand(string lookAt)
        {
            LookAt = lookAt;
        }

        private string LookAt { get; }

        public void Invoke()
        {
            if (LookAt == null)
            {
                dynamic loc = TextEngine.Player?.Location;
                loc.lookedAt = true;
                TextEngine.AddMessage(loc.LookDescription);
            }
        }
    }
}