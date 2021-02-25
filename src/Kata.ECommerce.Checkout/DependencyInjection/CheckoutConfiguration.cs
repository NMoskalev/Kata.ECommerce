using System;
using System.Collections.Generic;
using AutoMapper;
using Kata.ECommerce.Checkout.Discounts;
using Kata.ECommerce.Checkout.Services;
using Kata.ECommerce.Core.Checkout;
using Kata.ECommerce.Core.Checkout.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Kata.ECommerce.Checkout.DependencyInjection
{
    public class CheckoutConfiguration
    {
        private static readonly Dictionary<string, Func<Discount, ICalculate>> _discounts =
            new Dictionary<string, Func<Discount, ICalculate>>
            {
                { "x2", (discount) => new TwoItemsDiscountType(discount)},
                { "x3", (discount) => new ThreeItemsDiscountType(discount)}
            };

        public static void Configure(ServiceCollection collection)
        {
            collection.AddTransient<ICheckout, CheckoutService>();
            collection.AddScoped<IDiscountEngine>(s => new DiscountEngine(
                s.GetService<IDiscountRepository>(),
                s.GetService<IMapper>(),
                GetDiscountType));
        }

        private static Func<Discount, ICalculate> GetDiscountType(string type)
        {
            return _discounts[type];
        }
    }
}
