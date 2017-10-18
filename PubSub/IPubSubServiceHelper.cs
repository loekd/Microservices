using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PubSub
{
    public interface IPubSubServiceHelper
    {
        Task<HttpResponseMessage> RegisterWithPublisher(string subscribeCallbackUrl, string eventType);

        Task<HttpResponseMessage> RegisterWithPublisher(string subscribeCallbackUrl, Type eventType);

        Task<HttpResponseMessage> PublishEvent(IEvent @event);

        Task<HttpResponseMessage> UnregisterWithPublisher();
    }
}