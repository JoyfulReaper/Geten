using System;
using System.Text;

namespace Geten.Core.Memory
{
    public struct MemorySlot
    {
        public byte[] Buffer { get; set; }
        public string Name { get; set; }
        public string ValueString { get; set; }

        public static implicit operator bool(MemorySlot slot)
        {
            return BitConverter.ToBoolean(slot.Buffer);
        }

        public static implicit operator int(MemorySlot slot)
        {
            return BitConverter.ToInt32(slot.Buffer);
        }

        public static implicit operator MemorySlot(int num)
        {
            return new MemorySlot { Buffer = BitConverter.GetBytes(num), ValueString = num.ToString() };
        }

        public static implicit operator MemorySlot(bool value)
        {
            return new MemorySlot { Buffer = BitConverter.GetBytes(value), ValueString = value.ToString() };
        }

        public static implicit operator MemorySlot(string value)
        {
            return new MemorySlot { Buffer = Encoding.UTF8.GetBytes(value), ValueString = value.ToString() };
        }

        public static implicit operator string(MemorySlot slot)
        {
            return Encoding.UTF8.GetString(slot.Buffer);
        }

        public override string ToString() => ValueString;
    }
}