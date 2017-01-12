using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Numerics;
using Service.Models;

namespace Service.Analyzers
{
    public class AccountAnalyzer : IValidator<Account>
    {
        private const int AccountIdDefaultLength = 26;

        public bool IsInternalAccount(string accountId)
        {
            ValidateId(accountId, true);

            return accountId.Substring(2, 8) == ConfigurationManager.AppSettings["BankId"];
        }      

        public bool Validate(Account account)
        {
            ValidateId(account.Id, true);

            return true;
        }

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
