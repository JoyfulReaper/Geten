using System.Collections.Concurrent;
using TextEngine.Parsing.Syntax;

namespace TextEngine
{
    public static class EventManager
    {
        private static ConcurrentDictionary<string, ConcurrentQueue<BlockNode>> _subscriptions = new ConcurrentDictionary<string, ConcurrentQueue<BlockNode>>();

        public static void Subscribe(string name, BlockNode body)
        {
            if(_subscriptions.ContainsKey(name))
            {
                _subscriptions[name].Enqueue(body);
            }
            else
            {
                var bodyList = new ConcurrentQueue<BlockNode>();
                bodyList.Enqueue(body);
                _subscriptions.TryAdd(name, bodyList);
            }
        }

        public static void Raise(string name)
        {
            var bodies = _subscriptions[name];
            foreach (var b in bodies)
            {
                //b.Accept()// need a way to run the body
            }
        }
    }
}