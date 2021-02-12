namespace Kata.ECommerce.Core.Checkout.Models
{
    public class Item
    {
        public string ProductCode { get; set; }

        public double Price { get; set; }

        public override string ToString()
        {
            return $"Product Code: {ProductCode}, Price: {Price}";
        }
    }
}
