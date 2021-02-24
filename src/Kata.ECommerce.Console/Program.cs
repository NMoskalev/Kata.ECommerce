using System.Threading.Tasks;
using Kata.ECommerce.Core.Checkout;
using Kata.ECommerce.Core.Checkout.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Kata.ECommerce.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceProvider = Startup.Configure();
            var checkoutService = serviceProvider.GetService<ICheckout>();

            //please take a look at DiscountInMemoryRepository.cs where special offers are stored:
            var appleItem = new Item {ProductCode = "Apples", Price = 0.50};
            var bananaItem = new Item {ProductCode = "Bananas", Price = 0.70};
            var orangeItem = new Item {ProductCode = "Oranges", Price = 0.45};

            System.Console.WriteLine($"Following special offers will be applied:\nbananas: 2 for £1.00\noranges: 3 for £0.90");

            await checkoutService.Scan(appleItem);
            System.Console.WriteLine($"Item: {appleItem} has been added");
            System.Console.WriteLine($"Total: {await checkoutService.Total()}");

            await checkoutService.Scan(bananaItem);
            System.Console.WriteLine($"Item: {bananaItem} has been added");
            System.Console.WriteLine($"Total: {await checkoutService.Total()}");

            await checkoutService.Scan(orangeItem);
            System.Console.WriteLine($"Item: {orangeItem} has been added");
            System.Console.WriteLine($"Total: {await checkoutService.Total()}");

            await checkoutService.Scan(orangeItem);
            System.Console.WriteLine($"Item: {orangeItem} has been added");
            System.Console.WriteLine($"Total: {await checkoutService.Total()}");

            await checkoutService.Scan(bananaItem);
            System.Console.WriteLine($"Item: {bananaItem} has been added");
            System.Console.WriteLine($"Total: {await checkoutService.Total()}");

            await checkoutService.Scan(orangeItem);
            System.Console.WriteLine($"Item: {orangeItem} has been added");
            System.Console.WriteLine($"Total: {await checkoutService.Total()}");

            await checkoutService.Scan(appleItem);
            System.Console.WriteLine($"Item: {appleItem} has been added");
            System.Console.WriteLine($"Total: {await checkoutService.Total()}");

            await checkoutService.Remove(appleItem);
            System.Console.WriteLine($"Item: {appleItem} has been removed");
            System.Console.WriteLine($"Total: {await checkoutService.Total()}");

            System.Console.ReadLine();
        }
    }
}
