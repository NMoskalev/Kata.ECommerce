using System.Collections.Generic;

namespace Kata.ECommerce.Core.Checkout.Entities
{
    public class ShoppingCartEntity
    {
        public string UserId { get; set; }

        public List<LineItemEntity> LineItems { get; set; }

        public double Total { get; set; }

        public double SubTotal { get; set; }
    }
}
