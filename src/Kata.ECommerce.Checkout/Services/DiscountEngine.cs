using System;
using AutoMapper;
using Kata.ECommerce.Core.Checkout;
using Kata.ECommerce.Core.Checkout.Models;

namespace Kata.ECommerce.Checkout.Services
{
    internal class DiscountEngine : IDiscountEngine
    {
        private readonly IDiscountRepository _repository;
        private readonly IMapper _mapper;
        private readonly Func<string, Func<Discount, IDiscountType>> _types;

        public DiscountEngine(
            IDiscountRepository repository, 
            IMapper mapper, 
            Func<string, Func<Discount, IDiscountType>> types)
        {
            _repository = repository;
            _mapper = mapper;
            _types = types;
        }

        public void Apply(ShoppingCart cart)
        {
            var discounts = _repository.GetDiscounts();
            foreach (var discountDto in discounts)
            {
                var discount = _mapper.Map<Discount>(discountDto);
                //Todo: unreadable code.
                _types(discount.Type)(discount).Calculate(cart.LineItems);
            }
        }

        public void CleanUp(ShoppingCart cart)
        {
            foreach (var cartLineItem in cart.LineItems)
            {
                cartLineItem.Total = cartLineItem.SubTotal = cartLineItem.Price;
            }
        }
    }
}
