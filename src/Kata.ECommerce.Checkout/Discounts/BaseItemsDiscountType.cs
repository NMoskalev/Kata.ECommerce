using System;
using System.Collections.Generic;
using System.Linq;
using Kata.ECommerce.Core.Checkout;
using Kata.ECommerce.Core.Checkout.Models;

namespace Kata.ECommerce.Checkout.Discounts
{
    internal abstract class BaseItemsDiscountType : ICalculate
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

            //find out line items are followed by discount rules and not discount yet
            var discountItems = lineItems
                .Where(l => Discount.ProductCodes.Contains(l.ProductCode, StringComparer.OrdinalIgnoreCase)
                            && l.Total.Equals(l.SubTotal))
                .ToList();

            //check if we have enough line items to apply discount
            if (discountItems.Count >= ItemCount)
            {
                var totalAppliedItems = discountItems.Count;
                //prepare an array with split discount sum which gives the same sum in addition
                var sums = GetRoundSums(Discount.TotalPrice, ItemCount);
                // check if we have enough number of line items to apply discount
                while (totalAppliedItems / ItemCount > 0)
                {
                    for (var i = 1; i <= ItemCount; i++)
                    {
                        discountItems[totalAppliedItems - i].Total = sums[i - 1];
                    }
                    //discount was applied to a set of line items
                    totalAppliedItems -= ItemCount;
                }
            }
        }

        /// <summary>
        /// Split sum in equal or almost equal "number" parts, round it
        /// </summary>
        /// <param name="sum">split sum</param>
        /// <param name="number">number of part</param>
        /// <returns>return an array with split sum</returns>
        private static double[] GetRoundSums(double sum, int number)
        {
            var result = new double[number];
            var totalSum = sum;
            var individualSum = Math.Round(sum / number, 2);
            for (var i = 0; i < number; i++)
            {
                result[i] = Math.Round(totalSum - individualSum < individualSum ? totalSum : individualSum, 2);
                totalSum -= individualSum;
            }

            return result;
        }
    }
}
