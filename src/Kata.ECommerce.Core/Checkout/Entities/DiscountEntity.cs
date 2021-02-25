using System.Collections.Generic;

namespace Kata.ECommerce.Core.Checkout.Entities
{
    public class DiscountEntity
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public double TotalPrice { get; set; }

        public List<string> ProductCodes { get; set; }
    }
}
