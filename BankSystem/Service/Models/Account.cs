using System.Collections.Generic;
using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoRepository;
using Service.Generators;

namespace Service.Models
{
    /// <summary>
    /// Class represents account model
    /// </summary>
    [DataContract]
    public class Account : IEntity<string>
    {
        /// <summary>
        /// Gets or sets account database id
        /// </summary>
        [DataMember]
        [BsonId(IdGenerator = typeof(AccountIdGenerator))]
        public string Id { get; set; }
        /// <summary>
        /// Gets or sets account balance
        /// </summary>
        [DataMember]
        public decimal Balance { get; set; }
        /// <summary>
        /// Gets or sets owner id of account
        /// </summary>
        [DataMember]
        public string OwnerId { get; set; }
        /// <summary>
        /// Gets or sets list representing operations history for account
        /// </summary>
        [DataMember]
        public List<string> OperationsHistory { get; set; } = new List<string>();
    }
}
