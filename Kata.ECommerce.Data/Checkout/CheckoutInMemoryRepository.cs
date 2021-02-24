using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kata.ECommerce.Core.Checkout;
using Kata.ECommerce.Core.Checkout.Dto;

namespace Kata.ECommerce.Data.Checkout
{
    internal class CheckoutInMemoryRepository : ICheckoutRepository
    {
        //Todo: review question - why do we need a lock here?
        private readonly object _cartLock = new object();
        private static ShoppingCartDto _cart;

        public Task<ShoppingCartDto> GetShoppingCart()
        {
            if (_cart == null)
            {
                lock (_cartLock)
                {
                    _cart = new ShoppingCartDto() {LineItems = new List<LineItemDto>()};
                    return Task.FromResult(_cart);
                }
            }

            return Task.FromResult(_cart);
        }

        public Task<ShoppingCartDto> SaveShoppingCart(ShoppingCartDto cart)
        {
            if (cart == null)
            {
                throw new ArgumentNullException(nameof(cart));
            }

            lock (_cartLock)
            {
                _cart.LineItems = cart.LineItems;
                _cart.UserId = cart.UserId;
                _cart.SubTotal = cart.SubTotal;
                _cart.Total = cart.Total;

                return Task.FromResult(_cart);
            }
        }
    }
}
