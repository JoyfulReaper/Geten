namespace Geten.Commands
{
    internal class LookCommand : ITextCommand
    {
        public LookCommand(string lookAt)
        {
            LookAt = lookAt;
        }

        private string LookAt { get; set; }

        public void Invoke()
        {
            if (LookAt == null)
                TextEngine.AddMessage(TextEngine.Player?.Location.LookDescription);
        }
    }
}