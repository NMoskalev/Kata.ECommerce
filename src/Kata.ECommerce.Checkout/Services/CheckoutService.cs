using System;
using System.Linq;
using System.Runtime.CompilerServices;
using AutoMapper;
using Kata.ECommerce.Core.Checkout;
using Kata.ECommerce.Core.Checkout.Dto;
using Kata.ECommerce.Core.Checkout.Models;

[assembly: InternalsVisibleTo("Kata.ECommerce.Tests")]
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

        public void Scan(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var cart = GetCart();
            AddLineItem(cart, item);
            RecalculateTotal(cart);
        }

        public double Total()
        {
            return GetCart().Total;
        }

        public void Remove(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var cart = GetCart();
            RemoveLineItem(cart, item);
            _discountEngine.CleanUp(cart);
            RecalculateTotal(cart);
        }

        private ShoppingCart GetCart()
        {
            var cartDto = _repository.GetShoppingCart().Result;
            return _mapper.Map<ShoppingCart>(cartDto);
        }

        private void RecalculateTotal(ShoppingCart cart)
        {
            _discountEngine.Apply(cart);
            ApplyChanges(cart);
        }

        private void AddLineItem(ShoppingCart cart, Item item)
        {
            cart.LineItems.Add(new StandardLineItem
            {
                ProductCode = item.ProductCode,
                Price = item.Price,
                SubTotal = item.Price,
                Total = item.Price
            });
        }

        private void RemoveLineItem(ShoppingCart cart, Item item)
        {
            var lineItem = cart.LineItems.First(l =>
                l.ProductCode.Equals(item.ProductCode, StringComparison.OrdinalIgnoreCase));

            cart.LineItems.Remove(lineItem);
        }

        private void ApplyChanges(ShoppingCart cart)
        {
            cart.Total = Math.Round(cart.LineItems.Sum(l => l.Total), 2,MidpointRounding.ToPositiveInfinity);
            cart.SubTotal = Math.Round(cart.LineItems.Sum(l => l.SubTotal), 2, MidpointRounding.ToPositiveInfinity);

            _repository.SaveShoppingCart(_mapper.Map<ShoppingCartDto>(cart));
        }
    }
}
