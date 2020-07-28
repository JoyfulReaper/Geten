namespace Geten.Core
{
    public static class Optional
    {
        public static Optional<T> None<T>()
        {
            return new Optional<T>(null);
        }

        public static Optional<T> Some<T>(T value)
        {
            return new Optional<T>(value);
        }
    }

    public class Optional<T>
    {
        public Optional(object value)
        {
            if (value == null)
            {
                HasValue = false;
            }
            else
            {
                HasValue = true;
                Value = value;
            }
        }

        private bool HasValue { get; set; }
        private object Value { get; set; }

        public static implicit operator bool(Optional<T> v)
        {
            return v.HasValue;
        }

        public static implicit operator Optional<T>(T v)
        {
            return Optional.Some(v);
        }

        public static implicit operator T(Optional<T> v)
        {
            return (T)v.Value;
        }
    }
}