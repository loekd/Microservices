using System;
using System.Threading.Tasks;

namespace PubSub
{
    public class PubSubServiceEventPublisher : IEventPublisher
    {
        private readonly IPubSubServiceHelper _pubSubServiceHelper;

        public PubSubServiceEventPublisher(IPubSubServiceHelper pubSubServiceHelper)
        {
            _pubSubServiceHelper = pubSubServiceHelper ?? throw new ArgumentNullException(nameof(pubSubServiceHelper));
        }

        public async Task Publish(IEvent @event)
        {
            if (@event == null) throw new ArgumentNullException(nameof(@event));
            var result = await _pubSubServiceHelper.PublishEvent(@event);
            result.EnsureSuccessStatusCode();
        }
    }
}
