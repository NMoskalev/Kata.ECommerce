using Kata.ECommerce.Core.Checkout.Models;

namespace Kata.ECommerce.Checkout.Discounts
{
    internal class TwoItemsDiscountType : BaseItemsDiscountType
    {
        protected override int ItemCount => 2;

        public TwoItemsDiscountType(Discount discount) : base(discount)
        {
        }
    }
}
