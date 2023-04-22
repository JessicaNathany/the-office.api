using FluentValidation.Results;

namespace the_office.insfrastructure.Mediator.Message
{
    /// <summary>
    /// Define the base class response of the command
    /// </summary>
    public class CommandResponse<TResult>
    {
        public CommandResponse(TResult result, ValidationResult validationResult)
        {
            this.Result = result;
            this.Errors = validationResult == null ? Array.Empty<ValidationFailure>() : validationResult.Errors.ToArray();
        }

        /// <summary>
        /// Get errors list validation
        /// </summary>
        public IEnumerable<ValidationFailure> Errors { get; private set; }
        
        /// <summary>
        /// Gets an indicate valor if the command is valid
        /// </summary>
        public bool Valid =>  Errors.Any();

        /// <summary>
        /// Gets the result byd the command
        /// </summary>
        public TResult Result { get; private set; }


        /// <summary>
        /// The method return the default status validation
        /// </summary>
        /// <returns></returns>
        public int GetStatusCode()
        {
            if (!Errors.Any()) return 0;

            if (string.IsNullOrWhiteSpace(Errors.FirstOrDefault().ErrorCode)) return 0;

            if(!long.TryParse(Errors.FirstOrDefault().ErrorCode, out _)) return 0;

            var stringCode = Convert.ToInt32(Errors.First().ErrorCode);

            if (stringCode.ToString().Length >= 4)
                return Convert.ToInt32(stringCode.ToString().Substring(0, 3));

            return stringCode;
        }
    }
}
