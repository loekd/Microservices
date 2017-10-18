using System;
using System.Linq;
using System.Threading.Tasks;

namespace PubSub
{
    public abstract class EventSubscriber : IEventSubscriber
    {
        
        protected EventSubscriber(IEventPublisher publisher)
        {
            //register as subscriber, with publisher
            var attrib = GetType().GetCustomAttributes(typeof(HandlesEventTypeAttribute), false)
                .Cast<HandlesEventTypeAttribute>()
                .SingleOrDefault();

            foreach (var eventType in attrib?.EventTypes ?? Type.EmptyTypes)
            {
                publisher.RegisterSubscriber(eventType, this);
            }
        }

        public abstract Task Receive(IEvent @event);

    }
}