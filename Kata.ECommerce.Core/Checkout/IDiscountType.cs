using System.Collections.Generic;
using Kata.ECommerce.Core.Checkout.Models;

namespace Kata.ECommerce.Core.Checkout
{
    //Todo: rename it to ICalculate
    public interface IDiscountType
    {
        void Calculate(List<ILineItem> lineItems);
    }
}
