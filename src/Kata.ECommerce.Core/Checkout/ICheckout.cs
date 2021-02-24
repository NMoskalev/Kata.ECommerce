using Kata.ECommerce.Core.Checkout.Models;

namespace Kata.ECommerce.Core.Checkout
{
    public interface ICheckout
    {
        void Scan(Item item);

        double Total();

        void Remove(Item item);
    }
}
