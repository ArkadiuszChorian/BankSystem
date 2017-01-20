using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoRepository;

namespace Service.Models
{
    /// <summary>
    /// Class represents session model
    /// </summary>
    public class Session : IEntity<string>
    {
        /// <summary>
        /// Creates Session instance from given session id and user id
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="userId"></param>
        public Session(string sessionId, string userId)
        {
            SessionId = sessionId;
            UserId = userId;
        }

        /// <summary>
        /// Gets or sets session database id
        /// </summary>
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string Id { get; set; }
        /// <summary>
        /// Gets or sets session id
        /// </summary>
        public string SessionId { get; set; }
        /// <summary>
        /// Gets or sets user id
        /// </summary>
        public string UserId { get; set; }
    }
}
