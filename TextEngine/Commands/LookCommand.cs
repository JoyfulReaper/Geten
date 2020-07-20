namespace TextEngine.Commands
{
    [CommandName("look")]
    class LookCommand : ITextCommand
    {
        private string LookAt { get; set; }

        public LookCommand(string lookAt)
        {
            LookAt = lookAt;
        }

        public void Invoke()
        {
            if (LookAt == null)
                TextEngine.AddMessage(TextEngine.Player?.Location.LookDescription);
        }
    }
}
