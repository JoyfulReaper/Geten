using Geten.Core;
using System;

namespace Geten.Factories
{
    public class GameObjectFactory : IObjectFactory
    {
        public object Create<T>(params object[] args)
        {
            if (typeof(GameObject).IsAssignableFrom(typeof(T)))
            {
                return Activator.CreateInstance(typeof(T), args);
            }

            return null;
        }
    }
}