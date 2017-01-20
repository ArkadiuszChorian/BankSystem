using System;
using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoRepository;

namespace Service.Models
{
    [DataContract]
    public class Operation : IEntity<string>
    {
        public Operation(ExternalTransfer externalTransfer, string destinationId)
        {
            var amount = externalTransfer.Amount;
            var reminder = externalTransfer.Amount % 10;
            amount /= 10;
            reminder += 10 * (amount % 10);
            amount /= 10;

            Amount = amount + (decimal)reminder / 100;
            SourceId = externalTransfer.SourceId;
            DestinationId = destinationId;
            Title = externalTransfer.Title;
            OperationType = OperationTypes.Transfer;
        }   
            
        [DataMember]
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string Id { get; set; }
        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public DateTime DateTime { get; set; } = DateTime.Now;
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
        [DataMember]
        public OperationTypes OperationType { get; set; }

        public Operation Clone()
        {
            return (Operation)MemberwiseClone();
        }

        public enum OperationTypes
        {
            Transfer,
            Payment,
            Withdraw
        }
    }
}
