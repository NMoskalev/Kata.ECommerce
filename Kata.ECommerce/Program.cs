using System;
using Kata.ECommerce.Core.Checkout;
using Kata.ECommerce.Core.Checkout.Models;
using Kata.ECommerce.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Kata.ECommerce
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = Initializer.ConfigureServices(new ServiceCollection());
            var checkoutService = serviceProvider.GetService<ICheckout>();

            //please take a look at DiscountInMemoryRepository.cs where special offers are stored:
            var appleItem = new Item {ProductCode = "Apples", Price = 0.50};
            var bananaItem = new Item {ProductCode = "Bananas", Price = 0.70};
            var orangeItem = new Item {ProductCode = "Oranges", Price = 0.45};

            Console.WriteLine($"Following special offers will be applied:\nbananas: 2 for £1.00\noranges: 3 for £0.90");

            checkoutService.Scan(appleItem);
            Console.WriteLine($"Item: {appleItem} has been added");
            Console.WriteLine($"Total: {checkoutService.Total()}");

            checkoutService.Scan(bananaItem);
            Console.WriteLine($"Item: {bananaItem} has been added");
            Console.WriteLine($"Total: {checkoutService.Total()}");

            checkoutService.Scan(orangeItem);
            Console.WriteLine($"Item: {orangeItem} has been added");
            Console.WriteLine($"Total: {checkoutService.Total()}");

            checkoutService.Scan(orangeItem);
            Console.WriteLine($"Item: {orangeItem} has been added");
            Console.WriteLine($"Total: {checkoutService.Total()}");

            checkoutService.Scan(bananaItem);
            Console.WriteLine($"Item: {bananaItem} has been added");
            Console.WriteLine($"Total: {checkoutService.Total()}");

            checkoutService.Scan(orangeItem);
            Console.WriteLine($"Item: {orangeItem} has been added");
            Console.WriteLine($"Total: {checkoutService.Total()}");

            checkoutService.Scan(appleItem);
            Console.WriteLine($"Item: {appleItem} has been added");
            Console.WriteLine($"Total: {checkoutService.Total()}");

            checkoutService.Remove(appleItem);
            Console.WriteLine($"Item: {appleItem} has been removed");
            Console.WriteLine($"Total: {checkoutService.Total()}");

            Console.ReadLine();
        }
    }
}
