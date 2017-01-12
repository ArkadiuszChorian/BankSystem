using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service.Analyzers;

namespace Service.Tests
{
    [TestClass]
    public class AccountAnalyzerTest
    {
        [TestMethod]
        public void CreatingChecksumDigits()
        {
            var accountIdAnalyzer = new AccountAnalyzer();
            var bankId = ConfigurationManager.AppSettings["BankId"];
            var accountId = bankId + "0000000000000012";
            var checksum = accountIdAnalyzer.AppendChecksumDigits(accountId).Substring(0, 2);

            Assert.AreEqual("26", checksum);
        }
    }
}
