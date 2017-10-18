using System;
using System.Linq;

namespace PubSub
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class HandlesEventTypeAttribute : Attribute
    {
        public Type[] EventTypes { get; }

        public HandlesEventTypeAttribute(params Type[] eventTypes)
        {
            EventTypes = eventTypes ?? throw new ArgumentNullException(nameof(eventTypes));
            Type ievent = typeof(IEvent);
            if (eventTypes.Any(t => !ievent.IsAssignableFrom(t)))
            {
                throw new ArgumentException("eventTypes must contain only IEvent based types", nameof(eventTypes));
            }
        }
    }
}