using Newtonsoft.Json;
using the_office.domain.Enums;

namespace the_office.insfrastructure.Response
{
    /// <summary>
    /// Validation return class
    /// </summary>
    public class ValidationResponse
    {
        private readonly string _customMessage;

        public ValidationResponse()
        {
            Properties = null;
        }

        /// <summary>
        /// validation code
        /// </summary>
        [JsonProperty("errorCode")]
        public Enum Code { get; set; }

        public string Message 
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_customMessage))
                {
                    return Properties != null ? string.Format(_customMessage, Properties)
                                              : string.Format(_customMessage, Property, SourceValue);
                }

                return Properties != null ? string.Format("Error", Properties)
                                          : string.Format("Error", Property, SourceValue);
            }
        }

        /// <summary>
        /// Property where the validation was applied
        /// </summary>
        public string Property { get; set; }

        /// <summary>
        /// Value sent in the property that caused the error
        /// </summary>
        [JsonProperty("value")]
        public object SourceValue { get; set; }

        /// <summary>
        /// Property where the validation was applied
        /// </summary>
        public string[] Properties { get; set; }

        [JsonIgnore]
        public string CustomMessage => _customMessage;

        public static ValidationResponse CreateResponse(ErrorType errorType, string nameProperty, object value)
        {
            return new ValidationResponse
            {
                Code = errorType,
                Property = nameProperty,
                SourceValue = value
            };
        }
    }
}
