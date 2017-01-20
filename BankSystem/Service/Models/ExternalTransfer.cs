using System.Runtime.Serialization;

namespace Service.Models
{
    /// <summary>
    /// Class represents external transfer model
    /// </summary>
    [DataContract]
    public class ExternalTransfer
    {
        /// <summary>
        /// Creates ExternalTransfer instance from Operation object
        /// </summary>
        /// <param name="operation"></param>
        public ExternalTransfer(Operation operation)
        {
            Amount = decimal.ToInt32(100 * operation.Amount);
            SourceId = operation.SourceId;
            Title = operation.Title;
        }

        /// <summary>
        /// Gets or sets amount
        /// </summary>
        [DataMember(Name = "amount")]
        public int Amount { get; set; }

        /// <summary>
        /// Gets or sets source id
        /// </summary>
        [DataMember(Name = "from")]
        public string SourceId { get; set; }

        /// <summary>
        /// Gets or sets title
        /// </summary>
        [DataMember(Name = "title")]
        public string Title { get; set; }

        /// <summary>
        /// Returns JSON representation of object
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            return "{\"amount\":" + Amount + ",\"from\":\"" + SourceId + "\",\"title\":\"" + Title + "\"}";
        }
    }
}
