namespace OrderService.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string Product { get; set; }

        public int Quantity { get; set; }

        public int CustomerId { get; set; }
    }
}
