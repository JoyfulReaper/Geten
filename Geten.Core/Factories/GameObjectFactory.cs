using Geten.Core.Exceptions;
using Geten.Core.Parsers.Script.Syntax;
using System;
using System.Linq;

namespace Geten.Core.Factories
{
    public class GameObjectFactory : IObjectFactory
    {
        public object Create<T>(params object[] args)
        {
            GameObject instance = null;
            if (typeof(GameObject).IsAssignableFrom(typeof(T)))
            {
                if (args.Length > 2)
                {
                    instance = (GameObject)Activator.CreateInstance(typeof(T));
                    var map = instance.GetPropertyPositionMap();
                    if (map == null)
                    {
                        throw new ObjectFactoryException($"No property map exists for type: {instance.GetType().Name}");
                    }

                    if (args.Length > map.Count)
                    {
                        throw new ArgumentOutOfRangeException("More arguments supplied than expected.");
                    }

                    for (int i = 0; i < map.Count; i++)
                    {
                        if (i > args.Length)
                            break;

                        instance.SetProperty(map[i], args[i]);
                    }
                }
                else
                {
                    instance = (GameObject)Activator.CreateInstance(typeof(T));

                    if (args.Length == 2)
                    {
                        var name = args.First();
                        var props = (PropertyList)args.Last();

                        instance.SetProperty("name", name);
                        instance.MatchPropertyList(props);
                    }
                    else
                    {
                        var first = args.First();
                        if (first is string s)
                        {
                            instance.SetProperty("name", first);
                        }
                        else if (first is PropertyList pl)
                        {
                            instance.MatchPropertyList(pl);
                        }
                        else
                        {
                            throw new ObjectFactoryException("Ctors are only defined for string and ProperyList");
                        }
                    }
                }

                SymbolTable.Add(instance.Name, instance);
                return instance;
            }

            return null;
        }
    }
}