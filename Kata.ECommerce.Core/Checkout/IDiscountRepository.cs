using System.Collections.Generic;
using Kata.ECommerce.Core.Checkout.Dto;

namespace Kata.ECommerce.Core.Checkout
{
    public interface IDiscountRepository
    {
        IReadOnlyList<DiscountDto> GetDiscounts();
    }
}
