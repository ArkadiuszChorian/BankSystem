using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;
using MongoDB.Bson;
using MongoDB.Driver;
using Service.Models;

namespace Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "BankService" in both code and config file together.
    public class BankService : IBankService
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public string CreateUser(User user)
        {
            DAL.Instance.Users.InsertOne(user);

            return "OK";
        }

        public string CreateAccount(User user)
        {
            var userEntity = DAL.Instance.Users.AsQueryable().First(usr => usr.UserName == user.UserName);
            //DAL.Instance.Accounts.InsertOne(new Account(userEntity.Id));
            DAL.Instance.Accounts.InsertOne(new Account
            {
                OwnerId = userEntity.Id
            });

            return "OK";
        }

        public string Transfer(Operation operation)
        {
            //var s = DAL.Instance.Configurations.AsQueryable().First(config => config.Key == "CurrentAccountId").Value;
            var sourceAccount = DAL.Instance.Accounts.AsQueryable().Single(account => account.Id == operation.SourceId);
            var destinationAccount = DAL.Instance.Accounts.AsQueryable().Single(account => account.Id == operation.DestinationId);
            var operationInDestinationView = operation.Clone();

            operation.BalanceBefore = sourceAccount.Balance;
            operationInDestinationView.BalanceBefore = destinationAccount.Balance;

            sourceAccount.Balance -= operation.Amount;
            destinationAccount.Balance += operation.Amount;

            operation.BalanceAfter = sourceAccount.Balance;
            operationInDestinationView.BalanceAfter = destinationAccount.Balance;

            DAL.Instance.Operations.InsertOne(operation);
            DAL.Instance.Operations.InsertOne(operationInDestinationView);

            sourceAccount.OperationsHistory.Add(operation.Id);
            destinationAccount.OperationsHistory.Add(operationInDestinationView.Id);

            //DAL.Instance.Accounts

            return "OK";
        }

        public string Payment(Operation operation)
        {
            return "OK";
        }

        public string RetriveTransfer(Operation operation)
        {
            return "OK";
        }
    }
}
