using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models
{
    [DataContract]
    public class ExternalOperation
    {
        public ExternalOperation(Operation operation)
        {
            Amount = decimal.ToInt32(100 * operation.Amount);
            SourceId = operation.SourceId;
            Title = operation.Title;
            //amount = decimal.ToInt32(100 * operation.Amount);
            //from = operation.SourceId;
            //title = operation.Title;
        }

        [DataMember(Name = "amount")]
        public int Amount { get; set; }

        [DataMember(Name = "from")]
        public string SourceId { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        //[DataMember]
        //public int amount { get; set; }

        //[DataMember]
        //public string from { get; set; }

        //[DataMember]
        //public string title { get; set; }
    }
}
