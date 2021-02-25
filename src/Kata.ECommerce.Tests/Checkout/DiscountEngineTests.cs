using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kata.ECommerce.Checkout.Mappers;
using Kata.ECommerce.Checkout.Services;
using Kata.ECommerce.Core.Checkout;
using Kata.ECommerce.Core.Checkout.Entities;
using Kata.ECommerce.Core.Checkout.Models;
using Moq;
using Xunit;

namespace Kata.ECommerce.Tests.Checkout
{
    public class DiscountEngineTests
    {
        [Fact(DisplayName = "Apply discounts")]
        public async Task ApplyTest()
        {
            var lineItems = new List<ILineItem>
            {
                new StandardLineItem{Price = 1.5, SubTotal = 1.5, Total = 1},
                new StandardLineItem{Price = 2.5, SubTotal = 2.5, Total = 1}
            };

            var cart = new ShoppingCart
            {
                LineItems = new List<ILineItem>(lineItems)
            };

            var discounts = new List<DiscountEntity>
            {
                new DiscountEntity {Name = "discount1", Type = "discount1"}
            };

            var calculateValidation = new Func<List<ILineItem>, bool>((items) =>
             {
                 return !items.Except(lineItems).Any();
             });

            var repositoryMock = new Mock<IDiscountRepository>(MockBehavior.Strict);
            repositoryMock.Setup(r => r.GetDiscounts()).ReturnsAsync(discounts);

            var discountType = new Mock<ICalculate>(MockBehavior.Strict);
            discountType.Setup(d => d.Calculate(It.Is<List<ILineItem>>(l => calculateValidation(l))));

            var engine = GetEngine(repositoryMock, (type) => { return discount => discountType.Object; });
            await engine.Apply(cart);
        }

        [Fact(DisplayName = "Clean up discounts")]
        public void CleanUpTest()
        {
            var lineItems = new List<ILineItem>
            {
                new StandardLineItem{Price = 1.5, SubTotal = 1.5, Total = 1},
                new StandardLineItem{Price = 2.5, SubTotal = 2.5, Total = 1}
            };

            var cart = new ShoppingCart
            {
                LineItems = new List<ILineItem>(lineItems)
            };

            var repositoryMock = new Mock<IDiscountRepository>(MockBehavior.Strict);
            var engine = GetEngine(repositoryMock, (type) => { return discount => null;});
            engine.CleanUp(cart);

            Assert.Equal(lineItems[0].Price, cart.LineItems[0].Total);
            Assert.Equal(lineItems[1].Price, cart.LineItems[1].Total);
        }

        private DiscountEngine GetEngine(IMock<IDiscountRepository> repositoryMock, Func<string, Func<Discount, ICalculate>> types)
        {
            var profile = new EntityMapper();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            var mapper = new Mapper(configuration);

            return new DiscountEngine(repositoryMock.Object, mapper, types);
        }
    }
}
