using Geten.Core.Exceptions;
using System;
using System.Collections.Generic;

namespace Geten.Core
{
    public static class ObjectFactory
    {
        private static Dictionary<Type, IObjectFactory> _factories = new Dictionary<Type, IObjectFactory>();

        public static T Create<T>(params object[] args)
        {
            var type = typeof(T);
            var baseType = GetBaseType<T>();
            if (_factories.ContainsKey(baseType))
            {
                return (T)_factories[baseType].Create<T>(args);
            }

            throw new ObjectFactoryException($"No Factory registered for Type '{type}'");
        }

        public static bool IsRegisteredFor<TObject>()
        {
            return _factories.ContainsKey(typeof(TObject));
        }

        public static void Register<TFactory, UResult>()
                    where TFactory : IObjectFactory
        {
            var type = typeof(UResult);
            if (!_factories.ContainsKey(type))
            {
                var instance = Activator.CreateInstance<TFactory>();
                _factories.Add(type, instance);
                return;
            }

            throw new ObjectFactoryException("Factory is already registered");
        }

        private static Type GetBaseType<T>()
        {
            foreach (var t in _factories.Keys)
            {
                if (t.IsAssignableFrom(typeof(T)))
                {
                    return t;
                }
            }

            return typeof(T);
        }
    }
}