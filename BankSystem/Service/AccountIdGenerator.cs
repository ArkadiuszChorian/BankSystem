using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
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
            var currentAccountIdConfigKeyValue = DAL.Instance.Configurations.First(config => config.Key == "CurrentAccountId");
            //var currentAccountIdAsString = currentAccountIdConfigKeyValue.Value;
            var currentAccountIdAsLong = long.Parse(currentAccountIdConfigKeyValue.Value);
            currentAccountIdAsLong++;
            var currentAccountIdAsString = currentAccountIdAsLong.ToString();

            while (currentAccountIdAsString.Length < 16)
            {
                currentAccountIdAsString = "0" + currentAccountIdAsString;
            }

            currentAccountIdConfigKeyValue.Value = currentAccountIdAsString;

            DAL.Instance.Configurations.Update(currentAccountIdConfigKeyValue);
            //DAL.Instance.Configurations.Update(config => config.Key == "CurrentAccountId", Builders<ConfigKeyValue>.Update.Set("Value", currentAccountIdAsString));

            return currentAccountIdAsString;
        }

        public bool IsEmpty(object id)
        {
            return string.IsNullOrEmpty(id?.ToString());
        }
    }
}
