using System;
using System.Linq;
using MongoRepository;
using Service.Models;

namespace Service
{
    /// <summary>
    /// Static class providing some common extensions
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Checks if string is encoded in base64
        /// </summary>
        public static bool IsBase64Encoded(this string str)
        {
            try
            {
                Convert.FromBase64String(str);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Method gets destination account form repository
        /// </summary>
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

        /// <summary>
        /// Method gets source account form repository
        /// </summary>
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
