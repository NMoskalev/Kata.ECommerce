using System.Threading.Tasks;
using Kata.ECommerce.Core.Checkout.Entities;

namespace Kata.ECommerce.Core.Checkout
{
    public interface ICheckoutRepository
    {
        Task<ShoppingCartEntity> GetShoppingCart();

        Task<ShoppingCartEntity> SaveShoppingCart(ShoppingCartEntity cart);
    }
}
