using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Service.Models;

namespace Service
{
    public class AccountIdGenerator : IIdGenerator
    {
        //private static readonly Lazy<AccountIdGenerator> Lazy = new Lazy<AccountIdGenerator>(() => new AccountIdGenerator());
        //public static AccountIdGenerator Instance => Lazy.Value;
        //private AccountIdGenerator()
        public AccountIdGenerator()
        {
            //currentAccountIdAsString = DAL.Instance.Configurations.AsQueryable().First(config => config.Key == "currentAccountIdAsString").Value;
        }

        //public string currentAccountIdAsString { get; set; }

        public object GenerateId(object container, object document)
        {
            //var currentAccountId = long.Parse(currentAccountIdAsString);
            var currentAccountIdAsString = DAL.Instance.Configurations.AsQueryable().First(config => config.Key == "CurrentAccountId").Value;
            var currentAccountIdAsLong = long.Parse(currentAccountIdAsString);
            currentAccountIdAsLong++;
            currentAccountIdAsString = currentAccountIdAsLong.ToString();

            while (currentAccountIdAsString.Length < 16)
            {
                currentAccountIdAsString = "0" + currentAccountIdAsString;
            }
            
            DAL.Instance.Configurations.UpdateOne(config => config.Key == "CurrentAccountId", Builders<ConfigKeyValue>.Update.Set("Value", currentAccountIdAsString));

            return currentAccountIdAsString;
        }

        public bool IsEmpty(object id)
        {
            return string.IsNullOrEmpty(id?.ToString());
        }
    }
}
