﻿using Geten.Parsers.Script.Syntax;
using System.Collections.Concurrent;

namespace Geten
{
    public class EventManager
    {
        private readonly ConcurrentDictionary<string, ConcurrentQueue<BlockNode>> _subscriptions = new ConcurrentDictionary<string, ConcurrentQueue<BlockNode>>();

        public void Raise(string name)
        {
            var bodies = _subscriptions[name];
            foreach (var b in bodies)
            {
                //b.Accept()// need a way to run the body
            }
        }

        public void Subscribe(string name, BlockNode body)
        {
            if (_subscriptions.ContainsKey(name))
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
    }
}