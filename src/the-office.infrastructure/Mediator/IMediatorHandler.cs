using the_office.insfrastructure.Mediator.Message;

namespace the_office.insfrastructure.Mediator
{
    public interface IMediatorHandler
    {
        Task PublishEvent<TEvent>(TEvent @event) where TEvent : Event;
    }
}
