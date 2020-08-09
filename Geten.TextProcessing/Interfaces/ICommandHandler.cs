namespace Geten.TextProcessing.Interfaces
{
	public interface ICommandHandler
	{
		public void Invoke(Command cmd);
	}
}