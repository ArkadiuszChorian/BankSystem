using System;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service.Providers;

namespace Service.Tests
{
    [TestClass]
    public class AccountIdAnalyzerTest
    {
        [TestMethod]
        public void CreatingCheckSumDigits()
        {
            var accountIdAnalyzer = new AccountIdAnalyzer();
            var bankId = ConfigurationManager.AppSettings["BankId"];
            var accountId = bankId + "0000000000000012";
            var checkSum = accountIdAnalyzer.CreateCheckSumDigits(accountId).Substring(0, 2);

            Assert.AreEqual("26", checkSum);
        }
    }
}
