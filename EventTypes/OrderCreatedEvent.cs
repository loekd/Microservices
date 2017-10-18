using PubSub;

namespace EventTypes
{
    public class OrderCreatedEvent : IEvent
    {
        public string Product { get; set; }

        public int Quantity { get; set; }
    }
}