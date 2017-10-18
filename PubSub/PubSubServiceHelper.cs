using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PubSub
{
    public class PubSubServiceHelper : IPubSubServiceHelper
    {
        private readonly string _httpEndpointPubsub;
        private readonly HttpClient _httpClient;

        public PubSubServiceHelper()
        {
            //TODO: move url to config
            _httpEndpointPubsub = "http://localhost:4000/api/pubsub";
            _httpClient = new HttpClient();
        }

        public Task<HttpResponseMessage> RegisterWithPublisher(string subscribeCallbackUrl, string eventType)
        {
            var subscription = new Subscription
            {
                CallbackUri = subscribeCallbackUrl,
                EventType = eventType
            };
            string json = JsonConvert.SerializeObject(subscription);
            return PostData(json);
        }

        public Task<HttpResponseMessage> RegisterWithPublisher(string subscribeCallbackUrl, Type eventType)
        {
            return RegisterWithPublisher(subscribeCallbackUrl, eventType.Name);
        }

        public Task<HttpResponseMessage> PublishEvent(IEvent @event)
        {
            string json = JsonConvert.SerializeObject(@event);
            return PostData(json, $"publish/{@event.GetType().Name}");
        }

        private Task<HttpResponseMessage> PostData(string json, string subUrl = null)
        {
            HttpContent content = new StringContent(json);
            content.Headers.ContentType.MediaType = "application/json";
            var httpEndpointPubsub = _httpEndpointPubsub;
            if (!string.IsNullOrWhiteSpace(subUrl))
            {
                httpEndpointPubsub = $"{httpEndpointPubsub.TrimEnd('/')}/{subUrl.TrimStart('/')}";
            }

            var result = _httpClient.PostAsync(httpEndpointPubsub, content);
            return result;
        }

        public Task<HttpResponseMessage> UnregisterWithPublisher()
        {
            //TODO: implement
            throw new NotImplementedException();
        }
    }
}