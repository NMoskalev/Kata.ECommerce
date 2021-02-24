using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kata.ECommerce.Core.Checkout;
using Kata.ECommerce.Core.Checkout.Dto;
using Kata.ECommerce.Core.Checkout.Models;

namespace Kata.ECommerce.Checkout.Services
{
    internal class CheckoutService : ICheckout
    {
        private readonly ICheckoutRepository _repository;
        private readonly IDiscountEngine _discountEngine;
        private readonly IMapper _mapper;

        public CheckoutService(
            ICheckoutRepository repository,
            IDiscountEngine discountEngine,
            IMapper mapper)
        {
            _repository = repository;
            _discountEngine = discountEngine;
            _mapper = mapper;
        }

        public async Task Scan(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var cart = await GetCart();
            AddLineItem(cart, item);
            await RecalculateTotal(cart);
        }

        public async Task<double> Total()
        {
            return (await GetCart()).Total;
        }

        public async Task Remove(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var cart = await GetCart();
            RemoveLineItem(cart, item);
            await _discountEngine.CleanUp(cart);
            await RecalculateTotal(cart);
        }

        private async Task<ShoppingCart> GetCart()
        {
            var cartDto = await _repository.GetShoppingCart();
            return _mapper.Map<ShoppingCart>(cartDto);
        }

        private async Task RecalculateTotal(ShoppingCart cart)
        {
            await _discountEngine.Apply(cart);
            await ApplyChanges(cart);
        }

        private async Task ApplyChanges(ShoppingCart cart)
        {
            cart.Total = Math.Round(cart.LineItems.Sum(l => l.Total), 2, MidpointRounding.ToPositiveInfinity);
            cart.SubTotal = Math.Round(cart.LineItems.Sum(l => l.SubTotal), 2, MidpointRounding.ToPositiveInfinity);

            await _repository.SaveShoppingCart(_mapper.Map<ShoppingCartDto>(cart));
        }

        private static void AddLineItem(ShoppingCart cart, Item item)
        {
            cart.LineItems.Add(new StandardLineItem
            {
                ProductCode = item.ProductCode,
                Price = item.Price,
                SubTotal = item.Price,
                Total = item.Price
            });
        }

        private static void RemoveLineItem(ShoppingCart cart, Item item)
        {
            var lineItem = cart.LineItems.First(l =>
                l.ProductCode.Equals(item.ProductCode, StringComparison.OrdinalIgnoreCase));

            cart.LineItems.Remove(lineItem);
        }
    }
}
