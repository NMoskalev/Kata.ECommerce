using System.Threading.Tasks;
using Kata.ECommerce.Core.Checkout.DTO;

namespace Kata.ECommerce.Core.Checkout
{
    public interface ICheckoutRepository
    {
        Task<ShoppingCartDto> GetShoppingCart();

        Task<ShoppingCartDto> SaveShoppingCart(ShoppingCartDto cart);
    }
}
