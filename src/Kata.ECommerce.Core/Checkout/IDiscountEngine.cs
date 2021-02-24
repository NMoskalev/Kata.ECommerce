using System.Threading.Tasks;
using Kata.ECommerce.Core.Checkout.Models;

namespace Kata.ECommerce.Core.Checkout
{
    public interface IDiscountEngine
    {
        Task Apply(ShoppingCart cart);

        Task CleanUp(ShoppingCart cart);
    }
}
