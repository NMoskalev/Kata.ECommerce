using Kata.ECommerce.Core.Checkout;
using Kata.ECommerce.DataAccess.Checkout;
using Microsoft.Extensions.DependencyInjection;

namespace Kata.ECommerce.DataAccess.DependencyInjection
{
    public class DataAccessConfiguration
    {
        public static void Configure(ServiceCollection collection)
        {
            collection.AddSingleton<ICheckoutRepository, CheckoutInMemoryRepository>();
            collection.AddSingleton<IDiscountRepository, DiscountInMemoryRepository>();
        }
    }
}
