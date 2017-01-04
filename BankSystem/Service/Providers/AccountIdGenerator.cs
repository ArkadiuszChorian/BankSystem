using System.Configuration;
using System.Linq;
using MongoDB.Bson.Serialization;

namespace Service.Providers
{
    public class AccountIdGenerator : IIdGenerator
    {
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
            var accountIdAnalyzer = new AccountIdAnalyzer();
            var fullAccountId = accountIdAnalyzer.CreateCheckSumDigits(accountIdWithBankId);

            return fullAccountId;
        }

        public bool IsEmpty(object id)
        {
            return string.IsNullOrEmpty(id?.ToString());
        }
    }
}
