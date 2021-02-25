using System.Collections.Generic;
using System.Threading.Tasks;
using Kata.ECommerce.Core.Checkout.Entities;

namespace Kata.ECommerce.Core.Checkout
{
    public interface IDiscountRepository
    {
        Task<IEnumerable<DiscountEntity>> GetDiscounts();
    }
}
