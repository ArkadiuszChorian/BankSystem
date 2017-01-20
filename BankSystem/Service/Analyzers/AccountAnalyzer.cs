using System;
using System.Configuration;
using System.Linq;
using System.Numerics;
using Service.Models;

namespace Service.Analyzers
{
    /// <summary>
    /// Class for analyzing accounts
    /// </summary>
    public class AccountAnalyzer : IValidator<Account>
    {
        private const int AccountIdDefaultLength = 26;

        /// <summary>
        /// Checks if account for given account id is interal
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public bool IsInternalAccount(string accountId)
        {
            ValidateId(accountId, true);

            return GetBankIdFromAccountId(accountId) == ConfigurationManager.AppSettings["BankId"];
        }
        /// <summary>
        /// Gets bank id from given account id
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public string GetBankIdFromAccountId(string accountId)
        {
            return accountId.Substring(2, 8);
        }
        /// <summary>
        /// Validates account
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public bool Validate(Account account)
        {
            ValidateId(account.Id, true);

            return true;
        }
        /// <summary>
        /// Calculates and appends chceksum digits to given account
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public string AppendChecksumDigits(string accountId)
        {
            ValidateId(accountId, false);

            var polishCode = ConfigurationManager.AppSettings["PolishCode"];
            var numberToSolve = BigInteger.Parse(accountId + polishCode + "00");
            var checksumDigits = (98 - numberToSolve%97).ToString();

            if (checksumDigits.Length == 1)
            {
                checksumDigits = "0" + checksumDigits;
            }

            return checksumDigits + accountId;
        }     
        /// <summary>
        /// Validates account id
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="containsChecksumDigits"></param>
        public void ValidateId(string accountId, bool containsChecksumDigits)
        {           
            if (string.IsNullOrEmpty(accountId))
            {
                throw new ArgumentException("Account id is empty or null.");
            }

            var offset = containsChecksumDigits ? 0 : 2;

            if (accountId.Length > AccountIdDefaultLength - offset)
            {
                throw new ArgumentException("Account id is too long.");
            }

            if (accountId.Length < AccountIdDefaultLength - offset)
            {
                throw new ArgumentException("Account id is too short.");
            }

            if (!accountId.All(char.IsDigit))
            {
                throw new ArgumentException("Account id contains non-numeric characters.");
            }

            if (containsChecksumDigits)
            {
                var assumedChecksum = accountId.Substring(0, 2);
                var recalculatedChecksum = AppendChecksumDigits(accountId.Substring(2)).Substring(0, 2);

                if (assumedChecksum != recalculatedChecksum)
                {
                    throw new ArgumentException("Account id checksum is incorrect.");
                }
            }          
        }       
    }
}
