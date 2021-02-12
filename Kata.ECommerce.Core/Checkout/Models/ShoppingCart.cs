using System.Collections.Generic;

namespace Kata.ECommerce.Core.Checkout.Models
{
    public class ShoppingCart
    {
        public string UserId { get; set; }

        public List<ILineItem> LineItems { get; set; }

        public double Total { get; set; }

        public double SubTotal { get; set; }
    }
}
