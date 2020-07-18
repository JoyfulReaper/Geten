using System;
using System.Collections.Generic;
using System.Text;

namespace TextEngine
{
    /// <summary>
    /// Represents an amount of Money
    /// </summary>
    public class Money
    {
        /// <summary>
        /// The amount of Money - Can be Negative
        /// </summary>
        public decimal Amount
        {
            get => amount;
            set => amount = value;
        }

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

        private decimal amount;
        private string name;

        public Money(string name, decimal amount)
        {
            Amount = amount;
            Name = name;
        }
    }
}
