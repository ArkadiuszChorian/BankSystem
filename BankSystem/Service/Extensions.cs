using System;
using System.Linq;
using MongoRepository;
using Service.Models;

namespace Service
{
    public static class Extensions
    {
        public static bool IsBase64Encoded(this string str)
        {
            try
            {
                var bytes = Convert.FromBase64String(str);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static Account GetDestinationAccount(this MongoRepository<Account, string> repository, string accountId)
        {
            Account resultAccount;
            try
            {
                resultAccount = DAL.Instance.Accounts.Single(account => account.Id == accountId);
            }
            catch (InvalidOperationException exception)
            {
                throw new InvalidOperationException("Destination account does not exist.", exception);
            }

            return resultAccount;
        }

        public static Account GetSourceAccount(this MongoRepository<Account, string> repository, string accountId)
        {
            Account resultAccount;
            try
            {
                resultAccount = DAL.Instance.Accounts.Single(account => account.Id == accountId);
            }
            catch (InvalidOperationException exception)
            {
                throw new InvalidOperationException("Source account does not exist.", exception);
            }

            return resultAccount;
        }
    }
}
