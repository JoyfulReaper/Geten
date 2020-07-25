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
                var instance = (GameObject)Activator.CreateInstance(typeof(T), args);
                SymbolTable.Add(instance.Name, instance);
                return instance;
            }

            return null;
        }
    }
}