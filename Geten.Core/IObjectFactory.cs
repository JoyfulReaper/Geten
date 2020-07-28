namespace Geten.Core
{
    public interface IObjectFactory
    {
        object Create<T>(params object[] args);
    }
}