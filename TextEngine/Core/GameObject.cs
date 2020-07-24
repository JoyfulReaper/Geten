using Geten.Parsers.Script.Syntax;
using System.Collections;
using System.Collections.Generic;

namespace Geten.Core
{
    public abstract class GameObject : IEnumerable
    {
        //property bag for mutable properties by script
        private readonly Dictionary<CaseInsensitiveString, object> _properties = new Dictionary<CaseInsensitiveString, object>();

        public GameObject(string name, string descr)
        {
            Name = name;
            Description = descr;
        }

        public string Description
        {
            get { return GetProperty<string>(nameof(Description)); }
            set { SetProperty(nameof(Description), value); }
        }

        public string Name
        {
            get { return GetProperty<string>(nameof(Name)); }
            set { SetProperty(nameof(Name), value); }
        }

        public int PropertyCount => _properties.Count;

        public static T Create<T>(params object[] args)
                                            where T : GameObject
        {
            return ObjectFactory.Create<T>(args);
        }

        public static T Create<T>(PropertyList props)
                                            where T : GameObject
        {
            var instance = ObjectFactory.Create<T>();
            instance.MatchPropertyList(props);

            return instance;
        }

        public void Add(string name, object value)
        {
            SetProperty(name, value);
        }

        public IEnumerator GetEnumerator()
        {
            return _properties.GetEnumerator();
        }

        public T GetProperty<T>(string name)
        {
            if (_properties.ContainsKey(name))
            {
                return (T)_properties[name];
            }

            return default;
        }

        public void MatchPropertyList(PropertyList list)
        {
            foreach (var p in list)
            {
                SetProperty(p.Key.Text, list[p.Key.Text]);
            }
        }

        public void SetProperty(string name, object value)
        {
            if (_properties.ContainsKey(name))
            {
                _properties[name] = value;
            }
            else
            {
                _properties.Add(name, value);
            }
        }
    }
}