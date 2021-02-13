using System.Collections.Generic;
using Kata.ECommerce.Checkout.Discounts;
using Kata.ECommerce.Core.Checkout.Models;
using Xunit;

namespace Kata.ECommerce.Tests.Checkout
{
    public class ThreeItemsDiscountTypeTests
    {
        [Fact(DisplayName = "if no line items")]
        public void ThreeItemsDiscountTypeNegativeTest()
        {
            var discountType = new ThreeItemsDiscountType(GetDiscount());

            discountType.Calculate(null);
            discountType.Calculate(new List<ILineItem>());
        }

        [Fact(DisplayName = "3 line items but not following conditions")]
        public void ThreeItemsDiscountTypeEmptyTest()
        {
            var productCodes = new List<string> { "code1" };
            var discountType = new ThreeItemsDiscountType(GetDiscount(productCodes));
            var originalLineItems = new List<ILineItem>
            {
                new StandardLineItem {Price = 1, ProductCode = "code2"},
                new StandardLineItem {Price = 1, ProductCode = "code3"},
                new StandardLineItem {Price = 1, ProductCode = "code4"},
            };

            var lineItems = new List<ILineItem>();
            originalLineItems.ForEach(lineItems.Add);

            discountType.Calculate(lineItems);
            Assert.Equal(originalLineItems, lineItems);
        }

        [Fact(DisplayName = "3 line items but some of them already discounted")]
        public void ThreeItemsDiscountTypeAlreadyDiscountedTest()
        {
            var productCodes = new List<string> { "code1", "code2" };
            var discountType = new ThreeItemsDiscountType(GetDiscount(productCodes));
            var originalLineItems = new List<ILineItem>
            {
                new StandardLineItem {Price = 1, ProductCode = "code1", SubTotal = 1, Total = 0.5},
                new StandardLineItem {Price = 1, ProductCode = "code2", SubTotal = 1, Total = 1},
                new StandardLineItem {Price = 1, ProductCode = "code1", SubTotal = 1, Total = 1},
            };

            var lineItems = new List<ILineItem>();
            originalLineItems.ForEach(lineItems.Add);

            discountType.Calculate(lineItems);
            Assert.Equal(originalLineItems, lineItems);
        }

        [Fact(DisplayName = "2 line items")]
        public void ThreeItemsDiscountTypeTwoItemsTest()
        {
            var productCodes = new List<string> { "code1", "code2" };
            var discountType = new ThreeItemsDiscountType(GetDiscount(productCodes));
            var originalLineItems = new List<ILineItem>
            {
                new StandardLineItem {Price = 1, ProductCode = "code1", SubTotal = 1, Total = 1},
                new StandardLineItem {Price = 1, ProductCode = "code2", SubTotal = 1, Total = 1},
            };

            var lineItems = new List<ILineItem>();
            originalLineItems.ForEach(lineItems.Add);

            discountType.Calculate(lineItems);
            Assert.Equal(originalLineItems, lineItems);
        }

        [Fact(DisplayName = "3 line items and all following conditions")]
        public void ThreeItemsDiscountTypeThreeItemTest()
        {
            var productCodes = new List<string> { "code1", "code2" };
            var discountType = new ThreeItemsDiscountType(GetDiscount(productCodes));
            var lineItems = new List<ILineItem>
            {
                new StandardLineItem {Price = 1, ProductCode = "code1", SubTotal = 1, Total = 1},
                new StandardLineItem {Price = 1, ProductCode = "code2", SubTotal = 1, Total = 1},
                new StandardLineItem {Price = 1, ProductCode = "code1", SubTotal = 1, Total = 1},
            };

            discountType.Calculate(lineItems);
            Assert.Equal(0.34, lineItems[0].Total);
            Assert.Equal(0.33, lineItems[1].Total);
            Assert.Equal(0.33, lineItems[2].Total);
        }

        [Fact(DisplayName = "4 line items and all following conditions")]
        public void ThreeItemsDiscountTypeFourItemTest()
        {
            var productCodes = new List<string> { "code1", "code2" };
            var discountType = new ThreeItemsDiscountType(GetDiscount(productCodes));
            var lineItems = new List<ILineItem>
            {
                new StandardLineItem {Price = 1, ProductCode = "code1", SubTotal = 1, Total = 1},
                new StandardLineItem {Price = 1, ProductCode = "code2", SubTotal = 1, Total = 1},
                new StandardLineItem {Price = 1, ProductCode = "code1", SubTotal = 1, Total = 1},
                new StandardLineItem {Price = 1, ProductCode = "code1", SubTotal = 1, Total = 1},
            };

            discountType.Calculate(lineItems);
            Assert.Equal(1, lineItems[0].Total);
            Assert.Equal(0.34, lineItems[1].Total);
            Assert.Equal(0.33, lineItems[2].Total);
            Assert.Equal(0.33, lineItems[3].Total);
        }

        [Fact(DisplayName = "6 line items and all following conditions")]
        public void ThreeItemsDiscountTypeSixItemTest()
        {
            var productCodes = new List<string> { "code1", "code2" };
            var discountType = new ThreeItemsDiscountType(GetDiscount(productCodes));
            var lineItems = new List<ILineItem>
            {
                new StandardLineItem {Price = 1, ProductCode = "code1", SubTotal = 1, Total = 1},
                new StandardLineItem {Price = 1, ProductCode = "code2", SubTotal = 1, Total = 1},
                new StandardLineItem {Price = 1, ProductCode = "code1", SubTotal = 1, Total = 1},
                new StandardLineItem {Price = 1, ProductCode = "code1", SubTotal = 1, Total = 1},
                new StandardLineItem {Price = 1, ProductCode = "code1", SubTotal = 1, Total = 1},
                new StandardLineItem {Price = 1, ProductCode = "code1", SubTotal = 1, Total = 1},
            };

            discountType.Calculate(lineItems);
            Assert.Equal(0.34, lineItems[0].Total);
            Assert.Equal(0.33, lineItems[1].Total);
            Assert.Equal(0.33, lineItems[2].Total);
            Assert.Equal(0.34, lineItems[3].Total);
            Assert.Equal(0.33, lineItems[4].Total);
            Assert.Equal(0.33, lineItems[5].Total);
        }


        private static Discount GetDiscount(List<string> productCodes = null)
        {
            productCodes ??= new List<string> { "code1", "code2" };
            return new Discount
            {
                Name = "TestDiscount",
                ProductCodes = productCodes,
                TotalPrice = 1.0,
                Type = "type"
            };
        }
    }
}
