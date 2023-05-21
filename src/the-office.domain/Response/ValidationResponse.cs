using Newtonsoft.Json;
using the_office.domain.Enums;

namespace the_office.domain.Response
{
    public class ValidationResponse
    {
        private readonly string _customMessage;

        public ValidationResponse()
        {
            Properties = null;
        }

        public ValidationResponse(string customMessage)
        {
            Properties = null;
            _customMessage = customMessage;
        }

        /// <summary>
        /// Check code
        /// </summary>
        [JsonProperty("ErrorCode")]
        public Enum Code { get; set; }

        /// <summary>
        /// Validation message.
        /// </summary>
        [JsonProperty("description")]
        public string Message
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_customMessage))
                {
                    return Properties != null ? string.Format(_customMessage, Properties)
                                              : string.Format(_customMessage, Property, SourceValue);
                }

                return Properties != null ? string.Format("message resource include", Properties)
                                          : string.Format("message resource include", Property, SourceValue);
            }
        }

        /// <summary>
        /// Property where the validation applyed  
        /// </summary>
        public string Property { get; set; }

        /// <summary>
        /// Value send in property that origin the error
        /// </summary>
        [JsonProperty("value")]
        public object SourceValue { get; set; }

        /// <summary>
        /// Property where the validation applyed  
        /// </summary>
        [JsonIgnore]
        public string[] Properties { get; set; }

        [JsonIgnore]
        public string MensagemCustomizada => _customMessage;

        public static ValidationResponse CreateResponse(ErrorType code, string propertyName, object value)
        {
            return new ValidationResponse
            {
                Code = code,
                Property = propertyName,
                SourceValue = value
            };
        }
    }
}
