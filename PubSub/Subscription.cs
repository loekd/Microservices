namespace PubSub
{
    public class Subscription
    {
        public int Id { get; set; }

        public string CallbackUri { get; set; }

        public string EventType { get; set; }
    }
}
