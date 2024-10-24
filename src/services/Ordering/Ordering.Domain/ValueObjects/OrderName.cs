using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.ValueObjects
{
    public record OrderName
    {
        public const int DefaultName = 5;
        public string Value { get; set; }
        private OrderName(string value)
        {
            Value = value;
        }
        public static OrderName Of(string value)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(value);
            ArgumentOutOfRangeException.ThrowIfNotEqual(value.Length, DefaultName);
            return new OrderName(value);
        }

    }
}
