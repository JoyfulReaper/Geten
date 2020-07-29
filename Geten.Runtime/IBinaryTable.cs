namespace Geten.Runtime
{
    public interface IBinaryTable
    {
        void Load(byte[] raw);
        byte[] Save();
    }
}