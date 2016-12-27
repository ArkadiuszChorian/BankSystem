using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;
using Service.Models;

namespace Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "BankService" in both code and config file together.
    public class BankService : IBankService
    {
        public string AuthenticateUser(string userName, string password)
        {
            var user = DAL.Instance.Users.First(user2 => user2.UserName == userName);
            if (user.Password == password)
            {
                return "OK";
            }

            return "ERR";
        }

        public string RegisterUser(User user)
        {
            DAL.Instance.Users.Add(user);

            return "OK";
        }

        public string CreateAccount(User user)
        {
            var userEntity = DAL.Instance.Users.First(usr => usr.UserName == user.UserName);
            //DAL.Instance.Accounts.InsertOne(new Account(userEntity.Id));
            var newAccount = new Account {OwnerId = userEntity.Id};
            DAL.Instance.Accounts.Add(newAccount);
            userEntity.Accounts.Add(newAccount.Id);
            DAL.Instance.Users.Update(userEntity);

            return "OK";
        }

        public string Transfer(Operation operation)
        {
            //var s = DAL.Instance.Configurations.AsQueryable().First(config => config.Key == "CurrentAccountId").Value;
            var sourceAccount = DAL.Instance.Accounts.Single(account => account.Id == operation.SourceId);
            var destinationAccount = DAL.Instance.Accounts.Single(account => account.Id == operation.DestinationId);
            var operationInDestinationView = operation.Clone();

            operation.BalanceBefore = sourceAccount.Balance;
            operationInDestinationView.BalanceBefore = destinationAccount.Balance;

            sourceAccount.Balance -= operation.Amount;
            destinationAccount.Balance += operation.Amount;

            operation.BalanceAfter = sourceAccount.Balance;
            operationInDestinationView.BalanceAfter = destinationAccount.Balance;

            DAL.Instance.Operations.Add(operation);
            DAL.Instance.Operations.Add(operationInDestinationView);

            sourceAccount.OperationsHistory.Add(operation.Id);
            destinationAccount.OperationsHistory.Add(operationInDestinationView.Id);

            DAL.Instance.Accounts.Update(sourceAccount);
            DAL.Instance.Accounts.Update(destinationAccount);

            return "OK";
        }

        public string Payment(Operation operation)
        {
            return "OK";
        }

        public IEnumerable<Operation> GetHistory(string accountId)
        {
            var account = DAL.Instance.Accounts.First(account2 => account2.Id == accountId);
            var operations = new List<Operation>();
            account.OperationsHistory.ForEach(operationId =>
            {
                operations.Add(DAL.Instance.Operations.First(operation => operation.Id == operationId));
            });

            return operations;
        }

        public string RetrieveTransfer(Operation operation)
        {
            return "OK";
        }
    }
}
