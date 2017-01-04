using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoRepository;

namespace Service.Models
{
    [DataContract]
    public class Operation : IEntity<string>
    {
        public Operation(ExternalOperation externalOperation, string destinationId)
        {
            var amount = externalOperation.Amount;
            var reminder = externalOperation.Amount % 10;
            amount /= 10;
            reminder += 10 * (amount % 10);
            amount /= 10;

            Amount = amount + (decimal)reminder / 100;
            SourceId = externalOperation.SourceId;
            DestinationId = destinationId;
            Title = externalOperation.Title;
        }   
            
        [DataMember]
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string Id { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public DateTime DateTime { get; set; }
        [DataMember]
        public string SourceId { get; set; }
        [DataMember]
        public string DestinationId { get; set; }
        [DataMember]
        public decimal Amount { get; set; }
        [DataMember]
        public decimal BalanceBefore { get; set; }
        [DataMember]
        public decimal BalanceAfter { get; set; }

        public Operation Clone()
        {
            return (Operation)MemberwiseClone();
        }
    }
}
