using System.Collections.Generic;
using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoRepository;
using Service.Generators;

namespace Service.Models
{
    [DataContract]
    public class Account : IEntity<string>
    {
        [DataMember]
        [BsonId(IdGenerator = typeof(AccountIdGenerator))]
        public string Id { get; set; }
        [DataMember]
        public decimal Balance { get; set; }
        [DataMember]
        public string OwnerId { get; set; }
        [DataMember]
        public List<string> OperationsHistory { get; set; } = new List<string>();
    }
}
