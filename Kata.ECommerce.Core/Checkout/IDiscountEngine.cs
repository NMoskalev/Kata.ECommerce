using Kata.ECommerce.Core.Checkout.Models;

namespace Kata.ECommerce.Core.Checkout
{
    public interface IDiscountEngine
    {
        void Apply(ShoppingCart cart);

        void CleanUp(ShoppingCart cart);
    }
}
