using System;
using Kata.ECommerce.Checkout.DependencyInjection;
using Kata.ECommerce.Checkout.Mappers;
using Kata.ECommerce.DataAccess.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Kata.ECommerce.Console
{
    public class Startup
    {
        public static IServiceProvider Configure()
        {
            var serviceCollection = new ServiceCollection();
            // can be added any standard dependencies
            serviceCollection.AddLogging();
            serviceCollection.AddAutoMapper(c => c.AddProfile(typeof(EntityMapper)));
            DataAccessConfiguration.Configure(serviceCollection);
            CheckoutConfiguration.Configure(serviceCollection);

            return serviceCollection.BuildServiceProvider();
        }
    }
}
