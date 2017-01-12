using System.Collections.Generic;
using System.Linq;
using Service.Models;

namespace Service.Managers
{
    public class AccountManager
    {
        public List<Account> GetUserAccounts(User user)
        {
            var accounts = new List<Account>();

            user.Accounts.ForEach(accountId =>
            {
                accounts.Add(DAL.Instance.Accounts.First(account => account.Id == accountId));
            });

            return accounts;
        }

        public List<Operation> GetAccountHistory(string accountId)
        {
            var account = DAL.Instance.Accounts.Single(account2 => account2.Id == accountId);
            var operations = new List<Operation>();
            account.OperationsHistory.ForEach(operationId =>
            {
                operations.Add(DAL.Instance.Operations.Single(operation => operation.Id == operationId));
            });

            return operations;
        }

        public void CreateAccount(User user)
        {
            var newAccount = new Account { OwnerId = user.Id };
            DAL.Instance.Accounts.Add(newAccount);
            user.Accounts.Add(newAccount.Id);
            DAL.Instance.Users.Update(user);
        }

        public void DeleteAccount(string accountId)
        {
            var ownerId = DAL.Instance.Accounts.Single(account => account.Id == accountId).OwnerId;
            DAL.Instance.Accounts.Delete(accountId);
            var user = DAL.Instance.Users.First(user2 => user2.Id == ownerId);
            user.Accounts.Remove(accountId);
            DAL.Instance.Users.Update(user);
        }
    }
}
