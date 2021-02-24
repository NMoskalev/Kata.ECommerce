using System.Threading.Tasks;
using Kata.ECommerce.Core.Checkout.Dto;

namespace Kata.ECommerce.Core.Checkout
{
    //Todo: A repository should not return a DTO. It may return a domain model or entity or result object.
    public interface ICheckoutRepository
    {
        //Todo: GetShoppingCart(string cartId); or GetShoppingCart(string customerId);
        Task<ShoppingCartDto> GetShoppingCart();

        Task<ShoppingCartDto> SaveShoppingCart(ShoppingCartDto cart);
    }
}
