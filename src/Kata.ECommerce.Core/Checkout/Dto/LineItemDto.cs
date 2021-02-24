namespace Kata.ECommerce.Core.Checkout.Dto
{
    public class LineItemDto
    {
        public string ProductCode { get; set; }

        public double Price { get; set; }

        public double SubTotal { get; set; }

        public double Total { get; set; }
    }
}
