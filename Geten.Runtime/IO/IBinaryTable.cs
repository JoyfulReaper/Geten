namespace Geten.Runtime.IO
{
	public interface IBinaryTable
	{
		void Load(byte[] raw);

		byte[] Save();
	}
}