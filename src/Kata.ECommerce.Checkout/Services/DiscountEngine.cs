using System;
using System.Threading.Tasks;
using AutoMapper;
using Kata.ECommerce.Core.Checkout;
using Kata.ECommerce.Core.Checkout.Models;

namespace Kata.ECommerce.Checkout.Services
{
    internal class DiscountEngine : IDiscountEngine
    {
        private readonly IDiscountRepository _repository;
        private readonly IMapper _mapper;
        private readonly Func<string, Func<Discount, ICalculate>> _types;

        public DiscountEngine(
            IDiscountRepository repository, 
            IMapper mapper, 
            Func<string, Func<Discount, ICalculate>> types)
        {
            _repository = repository;
            _mapper = mapper;
            _types = types;
        }

        public async Task Apply(ShoppingCart cart)
        {
            var discounts = await _repository.GetDiscounts();
            foreach (var discountEntity in discounts)
            {
                var discount = _mapper.Map<Discount>(discountEntity);
                var discountTypeSelector = _types(discount.Type);
                var discountCalculation = discountTypeSelector(discount);
                discountCalculation.Calculate(cart.LineItems);
            }
        }

        public Task CleanUp(ShoppingCart cart)
        {
            foreach (var cartLineItem in cart.LineItems)
            {
                cartLineItem.Total = cartLineItem.SubTotal = cartLineItem.Price;
            }

            return Task.CompletedTask;
        }
    }
}
