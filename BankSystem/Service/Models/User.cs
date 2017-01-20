using System.Collections.Generic;
using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoRepository;

namespace Service.Models
{
    /// <summary>
    /// Class represents user model
    /// </summary>
    [DataContract]
    public class User : IEntity<string>
    {
        /// <summary>
        /// Gets or sets user database id
        /// </summary>
        [DataMember]
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string Id { get; set; }
        /// <summary>
        /// Gets or sets user name
        /// </summary>
        [DataMember]
        public string UserName { get; set; }
        /// <summary>
        /// Gets or sets password
        /// </summary>
        [DataMember]
        public string Password { get; set; }
        /// <summary>
        /// Gets or sets list of sessions ids
        /// </summary>
        [DataMember]
        public List<string> Sessions { get; set; } = new List<string>();
        /// <summary>
        /// Gets or sets list of accounts ids
        /// </summary>
        [DataMember]
        public List<string> Accounts { get; set; } = new List<string>();
    }
}
