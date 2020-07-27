using System;
using System.Collections.Generic;
using System.IO;

namespace Geten.Core.Memory
{
    public class SaveState
    {
        public List<MemorySlot> Slots = new List<MemorySlot>();

        public SaveState(params MemorySlot[] slots)
        {
            Slots = new List<MemorySlot>(slots);
        }

        public MemorySlot this[int index]
        {
            get
            {
                return Slots[index];
            }
            set
            {
                Slots[index] = value;
            }
        }

        public static SaveState Load(byte[] binary)
        {
            var result = new SaveState();
            var br = new BinaryReader(new MemoryStream(binary));

            var count = br.ReadInt16();
            for (int i = 0; i < count; i++)
            {
                var mscount = br.ReadInt16();

                var ms = new MemorySlot();
                ms.Buffer = br.ReadBytes(mscount);

                result.Slots.Add(ms);
            }

            return result;
        }

        public byte[] ToArray()
        {
            var result = new List<byte>();
            result.AddRange(BitConverter.GetBytes((byte)Slots.Count));

            foreach (var slot in Slots)
            {
                result.AddRange(BitConverter.GetBytes((byte)slot.Buffer.Length));
                result.AddRange(slot.Buffer);
            }

            return result.ToArray();
        }
    }
}