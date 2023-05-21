using FluentValidation.Results;
using MediatR;
using the_office.domain.Enums;
using the_office.insfrastructure.Response;

namespace the_office.insfrastructure.Mediator.Message
{
    public abstract class CommandHandler<TCommand, TResult> : IRequestHandler<TCommand, CommandResponse<TResult>> where TCommand : CommandHandler<TResult> 
    { 
        private readonly ValidationResult _validationResult;

        protected CommandHandler()
        {
            _validationResult = new ValidationResult();
        }

        public abstract Task<CommandResponse<TResult>> Handle(TCommand request, CancellationToken cancellationToken);
        public bool Valid => _validationResult.IsValid;

        protected void AddErrors(ErrorType errorType, string propertyName, object propertyValue)
        {
            var validationResponse = new ValidationResponse
            {
                Code = errorType,
                Property = propertyName,
                SourceValue= propertyValue
            };

            var validationFailure = new ValidationFailure(string.Empty, validationResponse.Message, validationResponse.SourceValue)
            {
                ErrorCode = errorType.ToString()
            };

            _validationResult.Errors.Add(validationFailure);
        }

        protected void AddGenericErrors(string customMessage, string nameProperty, object value)
        {
            var validationResponse = new ValidationResponse
            {
                Code = ErrorType.ValidationCustomMessage, 
                Property = nameProperty,  
                SourceValue= value
            };

            var validationFailure = new ValidationFailure(string.Empty, validationResponse.Message, validationResponse.SourceValue)
            {
                ErrorCode = ErrorType.ValidationCustomMessage.ToString()
            };

            _validationResult.Errors.Add(validationFailure);
        }

        // code review

        //protected CommandResponse<TResult> Response()
        //{
        //    return new CommandResponse<TResult>(default, _validationResult);
        //}

        //protected CommandResponse<TResult> Response(TResult result)
        //{
        //    return new CommandResponse<TResult>(result, _validationResult);
        //}
    }
}
