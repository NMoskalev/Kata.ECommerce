using System;
using System.Collections.Generic;
using AutoMapper;
using Kata.ECommerce.Checkout.Discounts;
using Kata.ECommerce.Checkout.Services;
using Kata.ECommerce.Core.Checkout;
using Kata.ECommerce.Core.Checkout.Models;
using StructureMap;

namespace Kata.ECommerce.Checkout.DependencyInjection
{
    public class CheckoutRegistry : Registry
    {
        private static readonly Dictionary<string, Func<Discount, IDiscountType>> _discounts =
            new Dictionary<string, Func<Discount, IDiscountType>>
            {
                { "x2", (discount) => new TwoBaseItemsDiscountType(discount)},
                { "x3", (discount) => new ThreeBaseItemsDiscountType(discount)}
            };

        public CheckoutRegistry()
        {
            For<ICheckout>().Use<CheckoutService>();
            For<IDiscountEngine>().Use(s => new DiscountEngine(
                s.GetInstance<IDiscountRepository>(),
                s.GetInstance<IMapper>(),
                GetDiscountType));
        }

        private static Func<Discount, IDiscountType> GetDiscountType(string type)
        {
            return _discounts[type];
        }
    }
}
