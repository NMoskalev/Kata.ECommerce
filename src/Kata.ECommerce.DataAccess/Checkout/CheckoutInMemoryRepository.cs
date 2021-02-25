using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kata.ECommerce.Core.Checkout;
using Kata.ECommerce.Core.Checkout.Entities;

namespace Kata.ECommerce.DataAccess.Checkout
{
    internal class CheckoutInMemoryRepository : ICheckoutRepository
    {
        private readonly object _cartLock = new object();
        private static ShoppingCartEntity _cart;

        public Task<ShoppingCartEntity> GetShoppingCart()
        {
            if (_cart == null)
            {
                lock (_cartLock)
                {
                    _cart = new ShoppingCartEntity() {LineItems = new List<LineItemEntity>()};
                    return Task.FromResult(_cart);
                }
            }

            return Task.FromResult(_cart);
        }

        public Task<ShoppingCartEntity> SaveShoppingCart(ShoppingCartEntity cart)
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
