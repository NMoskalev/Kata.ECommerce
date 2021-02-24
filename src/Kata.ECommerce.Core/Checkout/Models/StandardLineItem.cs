namespace Kata.ECommerce.Core.Checkout.Models
{
    public class StandardLineItem : ILineItem
    {
        public string ProductCode { get; set; }
        public double Price { get; set; }
        public double SubTotal { get; set; }
        public double Total { get; set; }
    }
}
