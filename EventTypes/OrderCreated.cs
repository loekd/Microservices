using PubSub;

namespace EventTypes
{
    public class OrderCreated : IEvent
    {
        public string Product { get; set; }

        public int Quantity { get; set; }
    }
}