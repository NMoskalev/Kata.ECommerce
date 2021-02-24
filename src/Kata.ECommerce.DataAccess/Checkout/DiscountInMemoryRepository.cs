﻿using Kata.ECommerce.Core.Checkout;
using Kata.ECommerce.Core.Checkout.Dto;
using System.Collections.Generic;

namespace Kata.ECommerce.Data.Checkout
{
    internal class DiscountInMemoryRepository : IDiscountRepository
    {
        private static readonly List<DiscountDto> _discounts = new List<DiscountDto>
        {
            new DiscountDto{ Name ="Bananas discount", Type = "x2", ProductCodes = new List<string>{"Bananas"}, TotalPrice = 1.00},
            new DiscountDto{ Name ="Oranges discount", Type = "x3", ProductCodes = new List<string>{"Oranges"}, TotalPrice = 0.90}
        };

        public IReadOnlyList<DiscountDto> GetDiscounts()
        {
            return _discounts;
        }
    }
}