namespace Kata.ECommerce.Core.Checkout.Entities
{
    public class LineItemEntity
    {
        public string ProductCode { get; set; }

        public double Price { get; set; }

        public double SubTotal { get; set; }

        public double Total { get; set; }
    }
}
