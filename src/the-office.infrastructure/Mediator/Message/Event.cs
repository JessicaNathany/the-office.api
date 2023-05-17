using MediatR;

namespace the_office.insfrastructure.Mediator.Message
{
    public abstract class Event : Message, INotification
    {
        public Event()
        {
            Timestamp = DateTime.Now;
        }

        public DateTime Timestamp { get; private set; } 
    }
}
