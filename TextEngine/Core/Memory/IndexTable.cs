using System;
using System.Collections.Generic;
using System.IO;

namespace Geten.Core.Memory
{
    public class IndexTable
    {
        Dictionary<string, int> _indices = new Dictionary<string, int>();

        public void Add(string key, int index)
        {

        }

        public byte[] ToArray()
        {
            var ms = new MemoryStream();
            var bw = new BinaryWriter(ms);

            bw.Write((byte)_indices.Count);
            foreach (var index in _indices)
            {
                bw.Write(index.Key);
                bw.Write(index.Value);
            }

            return ms.ToArray();
        }

        public static IndexTable Load(byte[] data)
        {
            var ms = new MemoryStream(data);
            var br = new BinaryReader(ms);
            var result = new IndexTable();

            var count = br.ReadInt16();
            for (int i = 0; i < count; i++)
            {
                var key = br.ReadString();
                var index = br.ReadInt32();

                result.Add(key, index);
            }

            return result;
        }
    }
}