namespace Geten.Core
{
	public class CaseSensisitiveString
	{
		private readonly string _src;

		public CaseSensisitiveString(string src)
		{
			this._src = src.ToLower();
			string.Intern(src);
		}

		public int Length => _src.Length;

		public static implicit operator CaseSensisitiveString(string str)
		{
			return new CaseSensisitiveString(str);
		}

		public static implicit operator string(CaseSensisitiveString str)
		{
			return str._src;
		}

		public static bool operator !=(CaseSensisitiveString src, string check)
		{
			return !src.Equals(check);
		}

		public static bool operator ==(CaseSensisitiveString src, string check)
		{
			return src.Equals(check);
		}

		public override bool Equals(object other)
		{
			if (!(other is CaseSensisitiveString)) return false;
			return Equals((CaseSensisitiveString)other);
		}

		public bool Equals(CaseSensisitiveString other)
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