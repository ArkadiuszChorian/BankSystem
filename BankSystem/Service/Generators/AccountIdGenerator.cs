using System.Configuration;
using System.Linq;
using MongoDB.Bson.Serialization;
using Service.Analyzers;

namespace Service.Generators
{
    /// <summary>
    /// Class for generating accounts ids
    /// </summary>
    public class AccountIdGenerator : IIdGenerator
    {
        /// <summary>
        /// Generates new account id
        /// </summary>
        /// <param name="container"></param>
        /// <param name="document"></param>
        /// <returns></returns>
        public object GenerateId(object container, object document)
        {
            var currentAccountIdConfigKeyValue = DAL.Instance.Configurations.First(config => config.Key == "CurrentAccountId");
            var currentAccountIdAsLong = long.Parse(currentAccountIdConfigKeyValue.Value);
            currentAccountIdAsLong++;
            var currentAccountIdAsString = currentAccountIdAsLong.ToString();

            while (currentAccountIdAsString.Length < 16)
            {
                currentAccountIdAsString = "0" + currentAccountIdAsString;
            }

            currentAccountIdConfigKeyValue.Value = currentAccountIdAsString;

            DAL.Instance.Configurations.Update(currentAccountIdConfigKeyValue);

            var accountIdWithBankId = ConfigurationManager.AppSettings["BankId"] + currentAccountIdAsString;
            var accountIdAnalyzer = new AccountAnalyzer();
            var fullAccountId = accountIdAnalyzer.AppendChecksumDigits(accountIdWithBankId);

            return fullAccountId;
        }
        /// <summary>
        /// Checks if given id is empty
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IsEmpty(object id)
        {
            return string.IsNullOrEmpty(id?.ToString());
        }
    }
}
