namespace TextEngine
{
    public class CaseInsensitiveString
    {
        private readonly string src;

        public CaseInsensitiveString(string src)
        {
            this.src = src.ToLower();
        }

        public static implicit operator string(CaseInsensitiveString str)
        {
            return str.src;
        }

        public static implicit operator CaseInsensitiveString(string str)
        {
            return new CaseInsensitiveString(str);
        }

        public static bool operator ==(CaseInsensitiveString src, string check)
        {
            return src.src == check;
        }

        public static bool operator !=(CaseInsensitiveString src, string check)
        {
            return src.src != check;
        }

        public override string ToString()
        {
            return src;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is CaseInsensitiveString other))
            {
                return false;
            }

            throw new System.NotImplementedException();
        }

        public override int GetHashCode()
        {
            return src.GetHashCode();
        }

        //ToDo: replace all needed strings
    }
}