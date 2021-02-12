using System.Collections.Generic;
using Kata.ECommerce.Core.Checkout.Dto;

namespace Kata.ECommerce.Core.Checkout.DTO
{
    public class ShoppingCartDto
    {
        public string UserId { get; set; }

        public List<LineItemDto> LineItems { get; set; }

        public double Total { get; set; }

        public double SubTotal { get; set; }
    }
}
