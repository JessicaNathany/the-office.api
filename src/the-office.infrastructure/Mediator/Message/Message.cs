namespace the_office.insfrastructure.Mediator.Message
{
    public abstract class Message
    {
        public Message()
        {
            MessageType = GetType().Name;
        }

        public string MessageType { get; protected set; }
        public Guid AggregateId { get; protected set; }
    }
}
