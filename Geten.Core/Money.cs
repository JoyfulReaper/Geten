namespace Geten.Core
{
	/// <summary>
	/// Represents an amount of Money
	/// </summary>
	public class Money
	{
		private string name;

		public Money() //needed for serialisation
		{
		}

		public Money(string name, decimal amount)
		{
			Amount = amount;
			Name = name;
		}

		/// <summary>
		/// The amount of Money - Can be Negative
		/// </summary>
		public decimal Amount { get; set; }

		/// <summary>
		/// The name of the Currency
		/// </summary>
		public string Name
		{
			get => name;
			set
			{
				if (value == null)
					value = "Gold";
				name = value;
			}
		}
	}
}