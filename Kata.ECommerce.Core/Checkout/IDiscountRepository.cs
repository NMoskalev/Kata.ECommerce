using System.Collections.Generic;
using Kata.ECommerce.Core.Checkout.Dto;

namespace Kata.ECommerce.Core.Checkout
{
    public interface IDiscountRepository
    {
        //Todo: A repository should not return a DTO. It may return a domain model or entity or result object.
        //Todo: IEnumerable<Discount> GetDiscounts(); would be better.
        IReadOnlyList<DiscountDto> GetDiscounts();
    }
}
