using FluentValidation.Results;
using MediatR;
using Newtonsoft.Json;

namespace the_office.insfrastructure.Mediator.Message
{
    public abstract class CommandHandler<TResult> : IRequest<CommandResponse<TResult>>
    {
        /// <summary>
        /// The list of errors resulting from the call to the is valid method
        /// </summary>

        [JsonIgnore]
        public ValidationResult ValidationResult { get; protected set; }

        /// <summary>
        /// Check if of the command is in a valid state
        /// </summary>
        /// <returns></returns>
        public virtual bool IsValid()
        {
            return true;
        }
    }
}
