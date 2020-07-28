using System;

namespace Geten.Core
{
    public static class Extensions
    {
        public static bool IsType<T>(this IObjectFactory f, Type t)
        {
            return t.IsAssignableFrom(typeof(T));
        }
    }
}