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
    }
}