using Newtonsoft.Json;

namespace the_office.domain.Response
{
    public class ObjectResponse
    {
        /// <summary>
        /// Class based on the basics of returning added an object to it.
        /// </summary>
        public ObjectResponse()
        {
            Validations = new List<ValidationResponse>();
        }

        /// <summary>
        /// Object that will be return.
        /// </summary>
        [JsonProperty("objeto")]
        public object Data { get; set; }

        [JsonProperty("ok")]
        public bool IsSuccess => Validations?.Any() != true;

        /// <summary>
        /// Collection of validations that will be return  
        /// </summary>
        [JsonProperty("validacoes")]
        public List<ValidationResponse> Validations { get; set; }

        /// <summary>
        /// Error checking method
        /// </summary>
        /// <returns>Verify result.</returns>
        public bool Any()
        {
            return this?.Validations?.Any() == true;
        }

        /// <summary>
        /// Checks if an error for a certain property is contained in the list 
        /// Checks if an error for a certain property is contained in the list.
        /// </summary>
        /// <param name="code">Error code.</param>
        /// <param name="property">Property name where is contained the erroe.</param>
        /// <returns>boolean check result.</returns>
        public bool ContainsError(Enum code, string property)
        {
            return Validations?.Any(e => Convert.ToInt64(e.Code) == Convert.ToInt64(code) && e.Property == property) == true;
        }
    }
}


