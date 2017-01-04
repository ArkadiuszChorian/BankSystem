using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Numerics;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Providers
{
    public class AccountIdAnalyzer
    {
        public bool IsInternalAccount(string accountId)
        {
            return accountId.Substring(2, 8) == ConfigurationManager.AppSettings["BankId"];
        }

        public bool IsValidId(string accountId)
        {
            return accountId.Length == 26 && IsCheckSumCorrect(accountId);
        }

        public string CreateCheckSumDigits(string accountId)
        {
            var polishCode = ConfigurationManager.AppSettings["PolishCode"];
            var numberToSolve = BigInteger.Parse(accountId + polishCode + "00");
            var checkSumDigits = (98 - numberToSolve % 97).ToString();

            if (checkSumDigits.Length == 1)
            {
                checkSumDigits = "0" + checkSumDigits;
            }

            return checkSumDigits + accountId;
        }

        public bool IsCheckSumCorrect(string accountId)
        {
            var assumedCheckSum = accountId.Substring(0, 2);
            var recalculatedCheckSum = CreateCheckSumDigits(accountId.Substring(2));

            return assumedCheckSum == recalculatedCheckSum;
        }    
    }
}
