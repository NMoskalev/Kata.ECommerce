using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kata.ECommerce.Checkout.Mappers;
using Kata.ECommerce.Checkout.Services;
using Kata.ECommerce.Core.Checkout;
using Kata.ECommerce.Core.Checkout.Dto;
using Kata.ECommerce.Core.Checkout.Models;
using Moq;
using Xunit;

namespace Kata.ECommerce.Tests.Checkout
{
    public class CheckoutServiceTests
    {
        [Fact(DisplayName = "Scan null Item")]
        public void ScanNullTest()
        {
            var service = GetService(new Mock<ICheckoutRepository>(), new Mock<IDiscountEngine>());
            Assert.ThrowsAsync<ArgumentNullException>(() => service.Scan(null));
        }

        [Fact(DisplayName = "Scan Item")]
        public async Task ScanItemTest()
        {
            var initialShoppingCart = new ShoppingCartDto {LineItems = new List<LineItemDto>()};
            var item = new Item { ProductCode = "Item1", Price = 1.33333 };
            var saveCartValidation = new Func<ShoppingCartDto, bool>((cart) =>
            {
                return cart.Total.Equals(1.34) &&
                       cart.SubTotal.Equals(1.34) &&
                       cart.LineItems[0].ProductCode.Equals(item.ProductCode);
            });

            var applyDiscountValidation = new Func<ShoppingCart, bool>((cart) =>
            {
                return cart.LineItems[0].ProductCode.Equals(item.ProductCode);
            });

            var checkoutRepositoryMock = new Mock<ICheckoutRepository>(MockBehavior.Strict);
            checkoutRepositoryMock.Setup(c => c.GetShoppingCart()).ReturnsAsync(initialShoppingCart);
            checkoutRepositoryMock.Setup(c =>
                    c.SaveShoppingCart(It.Is<ShoppingCartDto>(cart => saveCartValidation(cart))))
                .ReturnsAsync(It.IsAny<ShoppingCartDto>());

            var engineMock = new Mock<IDiscountEngine>(MockBehavior.Strict);
            engineMock.Setup(e => e.Apply(It.Is<ShoppingCart>(c => applyDiscountValidation(c)))).Returns(Task.CompletedTask);

            var service = GetService(checkoutRepositoryMock, engineMock);
            await service.Scan(item);
        }

        [Fact(DisplayName = "Remove null Item")]
        public void RemoveNullTest()
        {
            var service = GetService(new Mock<ICheckoutRepository>(), new Mock<IDiscountEngine>());
            Assert.ThrowsAsync<ArgumentNullException>(() => service.Remove(null));
        }

        [Fact(DisplayName = "Scan Item")]
        public async Task RemoveItemTest()
        {
            var item = new Item { ProductCode = "Item1", Price = 1.33333 };
            var initialShoppingCart = new ShoppingCartDto
            {
                LineItems = new List<LineItemDto> {new LineItemDto {ProductCode = item.ProductCode}}
            };

            var saveCartValidation = new Func<ShoppingCartDto, bool>((cart) =>
                {
                    return !cart.LineItems.Any(l => l.ProductCode.Equals(item.ProductCode));
                });

            var cleanUpDiscountValidation = new Func<ShoppingCart, bool>((cart) =>
            {
                return !cart.LineItems.Any(l => l.ProductCode.Equals(item.ProductCode));
            });

            var checkoutRepositoryMock = new Mock<ICheckoutRepository>(MockBehavior.Strict);
            checkoutRepositoryMock.Setup(c => c.GetShoppingCart()).ReturnsAsync(initialShoppingCart);
            checkoutRepositoryMock.Setup(c =>
                c.SaveShoppingCart(It.Is<ShoppingCartDto>(cart => saveCartValidation(cart))))
                .ReturnsAsync(It.IsAny<ShoppingCartDto>());

            var engineMock = new Mock<IDiscountEngine>(MockBehavior.Strict);
            engineMock.Setup(e => e.CleanUp(It.Is<ShoppingCart>(c => cleanUpDiscountValidation(c)))).Returns(Task.CompletedTask);
            engineMock.Setup(e => e.Apply(It.Is<ShoppingCart>(c => cleanUpDiscountValidation(c)))).Returns(Task.CompletedTask);

            var service = GetService(checkoutRepositoryMock, engineMock);
            await service.Remove(item);
        }

        [Fact(DisplayName="Get total")]
        public async Task GetTotalTest()
        {
            var total = 1.23;
            var initialShoppingCart = new ShoppingCartDto
            {
                Total = total
            };

            var checkoutRepositoryMock = new Mock<ICheckoutRepository>(MockBehavior.Strict);
            checkoutRepositoryMock.Setup(c => c.GetShoppingCart()).ReturnsAsync(initialShoppingCart);

            var engineMock = new Mock<IDiscountEngine>(MockBehavior.Strict);

            var service = GetService(checkoutRepositoryMock, engineMock);
            Assert.Equal(total, await service.Total());
        }

        private CheckoutService GetService(
            IMock<ICheckoutRepository> repository,
            IMock<IDiscountEngine> engine)
        {
            var profile = new EntityMapper();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            var mapper = new Mapper(configuration);

            var service = new CheckoutService(repository.Object, engine.Object, mapper);

            return service;
        }
    }
}
