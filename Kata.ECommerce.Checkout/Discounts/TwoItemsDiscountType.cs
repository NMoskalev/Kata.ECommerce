using Kata.ECommerce.Core.Checkout.Models;

namespace Kata.ECommerce.Checkout.Discounts
{
    internal class TwoBaseItemsDiscountType : BaseItemsDiscountType
    {
        protected override int ItemCount => 2;

        public TwoBaseItemsDiscountType(Discount discount) : base(discount)
        {
        }
    }
}
