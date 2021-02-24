using System.Collections.Generic;
using System.Threading.Tasks;
using Kata.ECommerce.Core.Checkout;
using Kata.ECommerce.Core.Checkout.Dto;

namespace Kata.ECommerce.DataAccess.Checkout
{
    internal class DiscountInMemoryRepository : IDiscountRepository
    {
        private static readonly List<DiscountDto> _discounts = new List<DiscountDto>
        {
            new DiscountDto{ Name ="Bananas discount", Type = "x2", ProductCodes = new List<string>{"Bananas"}, TotalPrice = 1.00},
            new DiscountDto{ Name ="Oranges discount", Type = "x3", ProductCodes = new List<string>{"Oranges"}, TotalPrice = 0.90}
        };

        public Task<List<DiscountDto>> GetDiscounts()
        {
            return Task.FromResult(_discounts);
        }
    }
}
