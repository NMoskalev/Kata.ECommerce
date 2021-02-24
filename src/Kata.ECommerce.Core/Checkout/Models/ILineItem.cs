using System;
using System.Collections.Generic;
using System.Text;

namespace Kata.ECommerce.Core.Checkout.Models
{
    public interface ILineItem
    {
        string ProductCode { get; set; }

        double Price { get; set; }

        double SubTotal { get; set; }

        double Total { get; set; }
    }
}
