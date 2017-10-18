using System;
using System.Threading.Tasks;

namespace PubSub
{
    public interface IEventPublisher
    {
        Task Publish(IEvent @event);

        void RegisterSubscriber(Type eventType, IEventSubscriber subscriber);

        void UnregisterSubscriber(Type eventType, IEventSubscriber subscriber);
    }
}