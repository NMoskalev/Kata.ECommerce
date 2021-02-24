using System.Threading.Tasks;
using Kata.ECommerce.Core.Checkout.Models;

namespace Kata.ECommerce.Core.Checkout
{
    public interface ICheckout
    {
        Task Scan(Item item);

        Task<double> Total();

        Task Remove(Item item);
    }
}
