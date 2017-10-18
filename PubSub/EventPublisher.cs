using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PubSub
{
    public class EventPublisher :  IEventPublisher
    {
        private readonly Dictionary<Type, HashSet<IEventSubscriber>> _registeredSubscribers = new Dictionary<Type, HashSet<IEventSubscriber>>();

        public async Task Publish(IEvent @event)
        {
            if (!_registeredSubscribers.TryGetValue(@event.GetType(), out var subscribers))
            {
                return;
            }
            //TODO: decouple here
            foreach (var eventSubscriber in subscribers)
            {
                await eventSubscriber.Receive(@event);
            }
        }

        public void RegisterSubscriber(Type eventType, IEventSubscriber subscriber)
        {
            if (!_registeredSubscribers.TryGetValue(eventType, out var subscribers))
            {
                subscribers = new HashSet<IEventSubscriber>();
                _registeredSubscribers.Add(eventType, subscribers);
            }

            subscribers.Add(subscriber);
        }

        public void UnregisterSubscriber(Type eventType, IEventSubscriber subscriber)
        {
            if (!_registeredSubscribers.TryGetValue(eventType, out var subscribers))
            {
                subscribers = new HashSet<IEventSubscriber>();
                _registeredSubscribers.Add(eventType, subscribers);
            }

            subscribers.Remove(subscriber);
        }
    }
}
