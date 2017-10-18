using System.Net.Http;
using System.Threading.Tasks;
using PubSub;

namespace OrderService
{
    public interface IPubSubServiceHelper
    {
        Task<HttpResponseMessage> RegisterWithPublisher(string subscribeCallbackUrl, string eventType);
        Task<HttpResponseMessage> PublishEvent(IEvent @event);
        Task<HttpResponseMessage> UnregisterWithPublisher();
    }
}