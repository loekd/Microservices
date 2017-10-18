using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Publisher.Controllers;
using PubSub;

namespace Publisher.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly List<Subscription> _subscriptions = new List<Subscription>();

        public int Count => _subscriptions.Count;

        public void Add(Subscription subscription)
        {
            //fake 'id'
            subscription.Id = Count + 1;
            _subscriptions.Add(subscription);
        }

        public void Delete(int id)
        {
            _subscriptions.RemoveAll(s => s.Id == id);
        }

        public IEnumerable<string> GetUrlsForEventType(string eventType)
        {
            return _subscriptions
                .Where(s => string.Equals(s.EventType, eventType, StringComparison.InvariantCultureIgnoreCase))
                .Select(s => s.CallbackUri);
        }
    }


    public interface ISubscriptionRepository
    {
        int Count { get; }

        void Add(Subscription subscription);


        void Delete(int id);

        IEnumerable<string> GetUrlsForEventType(string eventType);
    }
}
