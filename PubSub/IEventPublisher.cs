using System.Threading.Tasks;

namespace PubSub
{
    public interface IEventPublisher
    {
        Task Publish(IEvent @event);
    }
}