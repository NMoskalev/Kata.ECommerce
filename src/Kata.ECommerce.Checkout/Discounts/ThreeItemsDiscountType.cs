using Kata.ECommerce.Core.Checkout.Models;

namespace Kata.ECommerce.Checkout.Discounts
{
    internal class ThreeItemsDiscountType : BaseItemsDiscountType
    {
        protected override int ItemCount => 3;

        public ThreeItemsDiscountType(Discount discount) : base(discount)
        {
        }
    }
}
