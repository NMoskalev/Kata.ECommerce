using System.Collections.Generic;

namespace Kata.ECommerce.Core.Checkout.Dto
{
    public class ShoppingCartDto
    {
        public string UserId { get; set; }

        public List<LineItemDto> LineItems { get; set; }

        public double Total { get; set; }

        public double SubTotal { get; set; }
    }
}
