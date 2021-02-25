using System.Collections.Generic;
using System.Threading.Tasks;
using Kata.ECommerce.Core.Checkout;
using Kata.ECommerce.Core.Checkout.Entities;

namespace Kata.ECommerce.DataAccess.Checkout
{
    internal class DiscountInMemoryRepository : IDiscountRepository
    {
        private static readonly IEnumerable<DiscountEntity> _discounts = new List<DiscountEntity>
        {
            new DiscountEntity{ Name ="Bananas discount", Type = "x2", ProductCodes = new List<string>{"Bananas"}, TotalPrice = 1.00},
            new DiscountEntity{ Name ="Oranges discount", Type = "x3", ProductCodes = new List<string>{"Oranges"}, TotalPrice = 0.90}
        };

        public Task<IEnumerable<DiscountEntity>> GetDiscounts()
        {
            return Task.FromResult(_discounts);
        }
    }
}
