using System.Runtime.Serialization;

namespace Service.Models
{
    [DataContract]
    public class JsonError
    {
        [DataMember(Name = "error")]
        public string Error { get; set; }

        public JsonError(string errorMessage)
        {
            Error = errorMessage;
        }
    }
}
