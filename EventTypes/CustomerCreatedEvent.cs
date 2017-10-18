using PubSub;

namespace EventTypes
{
    public class CustomerCreatedEvent : IEvent
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
