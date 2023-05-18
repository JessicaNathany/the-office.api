using MediatR;
using the_office.insfrastructure.Mediator.Message;

namespace the_office.insfrastructure.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;   
        }

        public Task PublishEvent<TEvent>(TEvent @event) where TEvent : Event
        {
            return _mediator.Publish(@event);
        }
    }
}
