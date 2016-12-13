using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Service.Models
{
    [DataContract]
    public class Account
    {
        //public Account(){}

        //public Account(string ownerId)
        //{
        //    OwnerId = ownerId;
        //}

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
