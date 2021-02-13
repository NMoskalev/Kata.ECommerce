using System.Collections.Generic;
using Kata.ECommerce.Checkout.Discounts;
using Kata.ECommerce.Core.Checkout.Models;
using Xunit;

namespace Kata.ECommerce.Tests.Checkout
{
    public class TwoItemsDiscountTypeTests
    {

        [Fact(DisplayName = "if no line items")]
        public void TwoBaseItemsDiscountTypeNegativeTest()
        {
            var discountType = new TwoItemsDiscountType(GetDiscount());

            discountType.Calculate(null);
            discountType.Calculate(new List<ILineItem>());
        }

        [Fact(DisplayName = "2 line items but not following conditions")]
        public void TwoBaseItemsDiscountTypeEmptyTest()
        {
            var productCodes = new List<string> {"code1"};
            var discountType = new TwoItemsDiscountType(GetDiscount(productCodes));
            var originalLineItems = new List<ILineItem>
            {
                new StandardLineItem {Price = 1, ProductCode = "code2"},
                new StandardLineItem {Price = 1, ProductCode = "code3"},
            };

            var lineItems = new List<ILineItem>();
            originalLineItems.ForEach(lineItems.Add);

            discountType.Calculate(lineItems);
            Assert.Equal(originalLineItems, lineItems);
        }

        [Fact(DisplayName = "2 line items but already discounted")]
        public void TwoBaseItemsDiscountTypeAlreadyDiscountedTest()
        {
            var productCodes = new List<string> { "code1", "code2" };
            var discountType = new TwoItemsDiscountType(GetDiscount(productCodes));
            var originalLineItems = new List<ILineItem>
            {
                new StandardLineItem {Price = 1, ProductCode = "code1", SubTotal = 1, Total = 0.5},
                new StandardLineItem {Price = 1, ProductCode = "code2", SubTotal = 1, Total = 0.5},
            };

            var lineItems = new List<ILineItem>();
            originalLineItems.ForEach(lineItems.Add);

            discountType.Calculate(lineItems);
            Assert.Equal(originalLineItems, lineItems);
        }

        [Fact(DisplayName = "1 line items")]
        public void TwoItemsDiscountTypeOneItemTest()
        {
            var productCodes = new List<string> { "code1", "code2" };
            var discountType = new TwoItemsDiscountType(GetDiscount(productCodes));
            var originalLineItems = new List<ILineItem>
            {
                new StandardLineItem {Price = 1, ProductCode = "code1", SubTotal = 1, Total = 1},
            };

            var lineItems = new List<ILineItem>();
            originalLineItems.ForEach(lineItems.Add);

            discountType.Calculate(lineItems);
            Assert.Equal(originalLineItems, lineItems);
        }

        [Fact(DisplayName = "2 line items and both following conditions")]
        public void TwoItemsDiscountTypeTwoDifferentItemTest()
        {
            var productCodes = new List<string> { "code1", "code2" };
            var discountType = new TwoItemsDiscountType(GetDiscount(productCodes));
            var originalLineItems = new List<ILineItem>
            {
                new StandardLineItem {Price = 1, ProductCode = "code1", SubTotal = 1, Total = 1},
                new StandardLineItem {Price = 1, ProductCode = "code2", SubTotal = 1, Total = 1},
            };

            var lineItems = new List<ILineItem>();
            originalLineItems.ForEach(lineItems.Add);

            discountType.Calculate(lineItems);
            Assert.Equal(0.5, lineItems[0].Total);
            Assert.Equal(0.5, lineItems[1].Total);
        }

        [Fact(DisplayName = "3 line items but only two following conditions")]
        public void TwoItemsDiscountTypeThreeItemsTest()
        {
            var productCodes = new List<string> { "code1", "code2" };
            var discountType = new TwoItemsDiscountType(GetDiscount(productCodes));
            var originalLineItems = new List<ILineItem>
            {
                new StandardLineItem {Price = 1, ProductCode = "code3", SubTotal = 1, Total = 1},
                new StandardLineItem {Price = 1, ProductCode = "code1", SubTotal = 1, Total = 1},
                new StandardLineItem {Price = 1, ProductCode = "code2", SubTotal = 1, Total = 1},
            };

            var lineItems = new List<ILineItem>();
            originalLineItems.ForEach(lineItems.Add);

            discountType.Calculate(lineItems);
            Assert.Equal(0.5, lineItems[1].Total);
            Assert.Equal(0.5, lineItems[2].Total);
        }

        [Fact(DisplayName = "3 line items and all following conditions")]
        public void TwoItemsDiscountTypeAllThreeItemsTest()
        {
            var productCodes = new List<string> { "code1", "code2" };
            var discountType = new TwoItemsDiscountType(GetDiscount(productCodes));
            var originalLineItems = new List<ILineItem>
            {
                new StandardLineItem {Price = 1, ProductCode = "code1", SubTotal = 1, Total = 1},
                new StandardLineItem {Price = 1, ProductCode = "code1", SubTotal = 1, Total = 1},
                new StandardLineItem {Price = 1, ProductCode = "code2", SubTotal = 1, Total = 1},
            };

            var lineItems = new List<ILineItem>();
            originalLineItems.ForEach(lineItems.Add);

            discountType.Calculate(lineItems);
            Assert.Equal(1, lineItems[0].Total);
            Assert.Equal(0.5, lineItems[1].Total);
            Assert.Equal(0.5, lineItems[2].Total);
        }

        [Fact(DisplayName = "4 line items and all following conditions")]
        public void TwoItemsDiscountTypeAllFourItemsTest()
        {
            var productCodes = new List<string> { "code1", "code2" };
            var discountType = new TwoItemsDiscountType(GetDiscount(productCodes));
            var originalLineItems = new List<ILineItem>
            {
                new StandardLineItem {Price = 1, ProductCode = "code1", SubTotal = 1, Total = 1},
                new StandardLineItem {Price = 1, ProductCode = "code1", SubTotal = 1, Total = 1},
                new StandardLineItem {Price = 1, ProductCode = "code2", SubTotal = 1, Total = 1},
                new StandardLineItem {Price = 1, ProductCode = "code2", SubTotal = 1, Total = 1},
            };

            var lineItems = new List<ILineItem>();
            originalLineItems.ForEach(lineItems.Add);

            discountType.Calculate(lineItems);
            Assert.Equal(0.5, lineItems[0].Total);
            Assert.Equal(0.5, lineItems[1].Total);
            Assert.Equal(0.5, lineItems[2].Total);
            Assert.Equal(0.5, lineItems[3].Total);
        }


        private static Discount GetDiscount(List<string> productCodes = null)
        {
            productCodes ??= new List<string> {"code1", "code2"};
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
