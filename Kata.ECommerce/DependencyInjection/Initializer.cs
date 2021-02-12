using System;
using Kata.ECommerce.Checkout.Mappers;
using Microsoft.Extensions.DependencyInjection;
using StructureMap;

namespace Kata.ECommerce.DependencyInjection
{
    public class Initializer
    {
        //in real project (web or service) it can be initialized in Startup.cs just once, otherwise it can be wrapped up by Singleton
        public static IServiceProvider ConfigureServices(ServiceCollection serviceCollection)
        {
            // can be added any standard dependencies
            serviceCollection.AddLogging();
            serviceCollection.AddAutoMapper(c =>c.AddProfile(typeof(EntityMapper)));

            // add StructureMap
            var container = new Container();
            container.Configure(config =>
            {
                config.Scan(_ =>
                {
                    _.AssembliesFromApplicationBaseDirectory(a => a.FullName.StartsWith("Kata.ECommerce"));
                    _.LookForRegistries();
                    _.WithDefaultConventions();
                });
                config.Populate(serviceCollection);
            });

            return container.GetInstance<IServiceProvider>();
        }
    }
}
