using System.Runtime.Serialization;

namespace Service.Models
{
    [DataContract]
    public class ExternalTransfer
    {
        public ExternalTransfer(Operation operation)
        {
            Amount = decimal.ToInt32(100 * operation.Amount);
            SourceId = operation.SourceId;
            Title = operation.Title;
        }

        [DataMember(Name = "amount")]
        public int Amount { get; set; }

        [DataMember(Name = "from")]
        public string SourceId { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        public string ToJson()
        {
            return "{\"amount\":" + Amount + ",\"from\":\"" + SourceId + "\",\"title\":\"" + Title + "\"}";
        }
    }
}
