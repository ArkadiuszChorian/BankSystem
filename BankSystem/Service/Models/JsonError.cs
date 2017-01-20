using System.Runtime.Serialization;

namespace Service.Models
{
    /// <summary>
    /// Class represents json error model
    /// </summary>
    [DataContract]
    public class JsonError
    {
        /// <summary>
        /// Gets or sets error
        /// </summary>
        [DataMember(Name = "error")]
        public string Error { get; set; }

        /// <summary>
        /// Creates JsonError instance with given error message
        /// </summary>
        /// <param name="errorMessage"></param>
        public JsonError(string errorMessage)
        {
            Error = errorMessage;
        }
    }
}
