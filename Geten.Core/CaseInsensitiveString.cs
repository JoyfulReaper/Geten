namespace Geten.Core
{
	public class CaseInsensitiveString
	{
		private readonly string _src;

		public CaseInsensitiveString(string src)
		{
			this._src = src.ToLower();
			string.Intern(src);
		}

		public int Length => _src.Length;

		public static implicit operator CaseInsensitiveString(string str)
		{
			return new CaseInsensitiveString(str);
		}

		public static implicit operator string(CaseInsensitiveString str)
		{
			return str._src;
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
			return _src == other._src;
		}

		public override int GetHashCode()
		{
			return System.HashCode.Combine(_src);
		}

		public override string ToString()
		{
			return _src;
		}
	}
}