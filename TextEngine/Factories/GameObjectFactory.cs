using Geten.Core;
using Geten.Core.Exceptions;
using Geten.Parsers.Script.Syntax;
using System;
using System.Linq;

namespace Geten.Factories
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
                    throw new ObjectFactoryException("More arguments given than expected");
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