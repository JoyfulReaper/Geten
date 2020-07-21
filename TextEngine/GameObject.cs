using System.Collections;
using System.Collections.Generic;
using TextEngine.Parsing.Syntax;

namespace TextEngine
{
    public abstract class GameObject : IEnumerable
    {
        //property bag for changable properties by script
        private Dictionary<string, object> _properties = new Dictionary<string, object>();

        public void SetProperty(string name, object value)
        {
            if(_properties.ContainsKey(name))
            {
                _properties[name] = value;
            }
            else
            {
                _properties.Add(name, value);
            }
        }

        public T GetProperty<T>(string name)
        {
            return (T)GetProperty(name);
        }

        public object GetProperty(string name)
        {
            if(_properties.ContainsKey(name))
            {
                return _properties[name];
            }

            return null;
        }


        public string Name { get; set; }

        public int PropertyCount => _properties.Count;



        public void MatchPropertyList(PropertyList list)
        {
            foreach (var p in list)
            {
                SetProperty(p.Key.Text, list[p.Key.Text]);
            }
        }

        public IEnumerator GetEnumerator()
        {
            return _properties.GetEnumerator();
        }

        public void Add(string name, object value)
        {
            _properties.Add(name, value);
        }
    }
}