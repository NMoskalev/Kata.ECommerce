using System.Threading.Tasks;
using Kata.ECommerce.Core.Checkout.Dto;

namespace Kata.ECommerce.Core.Checkout
{
    public interface ICheckoutRepository
    {
        Task<ShoppingCartDto> GetShoppingCart();

        Task<ShoppingCartDto> SaveShoppingCart(ShoppingCartDto cart);
    }
}
