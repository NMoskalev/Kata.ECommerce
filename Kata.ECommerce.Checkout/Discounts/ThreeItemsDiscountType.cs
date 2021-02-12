using Kata.ECommerce.Core.Checkout.Models;

namespace Kata.ECommerce.Checkout.Discounts
{
    internal class ThreeBaseItemsDiscountType : BaseItemsDiscountType
    {
        protected override int ItemCount => 3;

        public ThreeBaseItemsDiscountType(Discount discount) : base(discount)
        {
        }
    }
}
