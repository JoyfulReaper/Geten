namespace Geten.TextProcessing
{
	public interface ICommandHandler
	{
		public void Invoke(Command cmd);
	}
}