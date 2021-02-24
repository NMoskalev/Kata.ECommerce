using System;
using System.Collections.Generic;
using System.Linq;
using Kata.ECommerce.Core.Checkout;
using Kata.ECommerce.Core.Checkout.Models;

namespace Kata.ECommerce.Checkout.Discounts
{
    internal abstract class BaseItemsDiscountType : IDiscountType
    {
        protected readonly Discount Discount;
        protected abstract int ItemCount { get; }

        protected BaseItemsDiscountType(Discount discount)
        {
            Discount = discount;
        }

        public virtual void Calculate(List<ILineItem> lineItems)
        {
            if (lineItems == null)
            {
                return;
            }

            var discountItems = lineItems
                .Where(l => Discount.ProductCodes.Contains(l.ProductCode, StringComparer.OrdinalIgnoreCase)
                            && l.Total.Equals(l.SubTotal))
                .ToList();

            if (discountItems.Count >= ItemCount)
            {
                var totalAppliedItems = discountItems.Count;
                var sums = GetRoundSums();
                while (totalAppliedItems / ItemCount > 0)
                {
                    for (var i = 1; i <= ItemCount; i++)
                    {
                        discountItems[totalAppliedItems - i].Total = sums[i - 1];
                    }

                    totalAppliedItems -= ItemCount;
                }
            }
        }

        private double[] GetRoundSums()
        {
            var result = new double[ItemCount];
            var totalSum = Discount.TotalPrice;
            var individualSum = Math.Round(Discount.TotalPrice / ItemCount, 2);
            for (var i = 0; i < ItemCount; i++)
            {
                result[i] = Math.Round(totalSum - individualSum < individualSum ? totalSum : individualSum, 2);
                totalSum -= individualSum;
            }

            return result;
        }
    }
}
