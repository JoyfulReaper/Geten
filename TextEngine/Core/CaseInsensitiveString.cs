namespace Geten.Core
{
    public class CaseInsensitiveString
    {
        private readonly string src;

        public CaseInsensitiveString(string src)
        {
            this.src = src.ToLower();
            string.Intern(src);
        }

        public int Length => src.Length;

        public static implicit operator CaseInsensitiveString(string str)
        {
            return new CaseInsensitiveString(str);
        }

        public static implicit operator string(CaseInsensitiveString str)
        {
            return str.src;
        }

        public static bool operator !=(CaseInsensitiveString src, string check)
        {
            return !src.Equals(check);
        }

        public static bool operator ==(CaseInsensitiveString src, string check)
        {
            return src.Equals(check);
        }

        public override bool Equals(object other)
        {
            if (!(other is CaseInsensitiveString)) return false;
            return Equals((CaseInsensitiveString)other);
        }

        public bool Equals(CaseInsensitiveString other)
        {
            return src == other.src;
        }

        public override int GetHashCode()
        {
            return src.GetHashCode();
        }

        public override string ToString()
        {
            return src;
        }
    }
}