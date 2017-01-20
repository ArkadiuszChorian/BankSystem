using System;
using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoRepository;

namespace Service.Models
{
    /// <summary>
    /// Class represents operation model
    /// </summary>
    [DataContract]
    public class Operation : IEntity<string>
    {
        /// <summary>
        /// Creates Operation instance from given ExternalTransfer and destination id
        /// </summary>
        /// <param name="externalTransfer"></param>
        /// <param name="destinationId"></param>
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

        /// <summary>
        /// Gets or sets account database id
        /// </summary>
        [DataMember]
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string Id { get; set; }
        /// <summary>
        /// Gets or sets title
        /// </summary>
        [DataMember]
        public string Title { get; set; }
        /// <summary>
        /// Gets or sets DateTime
        /// </summary>
        [DataMember]
        public DateTime DateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// Gets or sets source id
        /// </summary>
        [DataMember]
        public string SourceId { get; set; }
        /// <summary>
        /// Gets or sets destination id
        /// </summary>
        [DataMember]
        public string DestinationId { get; set; }
        /// <summary>
        /// Gets or sets amount
        /// </summary>
        [DataMember]
        public decimal Amount { get; set; }
        /// <summary>
        /// Gets or sets balance before
        /// </summary>
        [DataMember]
        public decimal BalanceBefore { get; set; }
        /// <summary>
        /// Gets or sets balance after
        /// </summary>
        [DataMember]
        public decimal BalanceAfter { get; set; }
        /// <summary>
        /// Gets or sets operation type
        /// </summary>
        [DataMember]
        public OperationTypes OperationType { get; set; }

        /// <summary>
        /// Makes shallow copy of object
        /// </summary>
        /// <returns></returns>
        public Operation Clone()
        {
            return (Operation)MemberwiseClone();
        }

        /// <summary>
        /// Defines enumeration for operation types
        /// </summary>
        public enum OperationTypes
        {
            Transfer,
            Payment,
            Withdraw
        }
    }
}
