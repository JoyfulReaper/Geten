using System;
using System.Collections.Generic;
using System.Text;
using TextEngine.CommandParsing;
using TextEngine.Parsing;

namespace TextEngine.Commands
{
    [CommandName("look")]
    class LookCommand : ITextCommand
    {
        private Token<CommandKind> LookAt { get; set; }
        public void Invoke()
        {
            if (LookAt == null)
                TextEngine.AddMessage(TextEngine.Player.Location.LookDescription);
        }

        public ITextCommand Parse(CommandParser parser)
        {
            try
            {
                LookAt = parser.MatchToken(CommandKind.Identifier);
            } catch (Exception e)
            {
                LookAt = null;
            }

            return new LookCommand();
        }
    }
}
