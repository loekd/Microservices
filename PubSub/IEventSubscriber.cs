using System.Threading.Tasks;

namespace PubSub
{
    public interface IEventSubscriber
    {
        Task Receive(IEvent @event);
    }
}