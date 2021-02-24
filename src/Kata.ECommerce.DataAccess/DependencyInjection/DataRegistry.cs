using Kata.ECommerce.Core.Checkout;
using Kata.ECommerce.Data.Checkout;
using StructureMap;

namespace Kata.ECommerce.Data.DependencyInjection
{
    public class DataRegistry : Registry
    {
        public DataRegistry()
        {
            For<ICheckoutRepository>().Singleton().Use<CheckoutInMemoryRepository>();
            For<IDiscountRepository>().Singleton().Use<DiscountInMemoryRepository>();
        }
    }
}
