using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoRepository;

namespace Service.Models
{
    /// <summary>
    /// Class represents configuration key-value model
    /// </summary>
    public class ConfigKeyValue : IEntity<string>
    {
        /// <summary>
        /// Gets or sets database id
        /// </summary>
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string Id { get; set; }
        /// <summary>
        /// Gets or sets key
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// Gets or sets value
        /// </summary>
        public string Value { get; set; }
    }
}
