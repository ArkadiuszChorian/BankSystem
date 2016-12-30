using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoRepository;

namespace Service.Models
{
    public class Session : IEntity<string>
    {
        public Session(string sessionId, string userId)
        {
            SessionId = sessionId;
            UserId = userId;
        }

        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string Id { get; set; }

        public string SessionId { get; set; }
        public string UserId { get; set; }
    }
}
