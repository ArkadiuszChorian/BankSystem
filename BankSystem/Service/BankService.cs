using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;
using Microsoft.AspNetCore.Mvc;
using Service.Models;
using Service.Providers;

namespace Service
{
    public class BankService : IBankService, IBankServiceWeb
    {
        public AccountIdAnalyzer AccountIdAnalyzer { get; set; } = new AccountIdAnalyzer();
        public TransferAndPaymentManager TransferAndPaymentManager { get; set; } = new TransferAndPaymentManager();

        public List<Account> GetAccounts(string sessionId)
        {
            var session = DAL.Instance.Sessions.First(session2 => session2.SessionId == sessionId);
            var user = DAL.Instance.Users.First(user2 => user2.Id == session.UserId);
            var accounts = new List<Account>();

            user.Accounts.ForEach(accountId =>
            {
                accounts.Add(DAL.Instance.Accounts.First(account => account.Id == accountId));
            });

            return accounts;
        }

        public List<string> GetAccountsIds(string sessionId)
        {
            var session = DAL.Instance.Sessions.First(session2 => session2.SessionId == sessionId);
            var user = DAL.Instance.Users.First(user2 => user2.Id == session.UserId);

            return user.Accounts;
        }

        public List<Operation> GetAccountHistory(string accountId)
        {
            var account = DAL.Instance.Accounts.First(account2 => account2.Id == accountId);
            var operations = new List<Operation>();
            account.OperationsHistory.ForEach(operationId =>
            {
                operations.Add(DAL.Instance.Operations.First(operation => operation.Id == operationId));
            });

            return operations;
        }

        public string GenerateSessionId(string userName)
        {
            var sessionIdGenerator = new SessionIdGenerator();
            var user = DAL.Instance.Users.First(user2 => user2.UserName == userName);
            var session = new Session(sessionIdGenerator.GenerateId(), user.Id);
            DAL.Instance.Sessions.Add(session);
            user.Sessions.Add(session.Id);
            DAL.Instance.Users.Update(user);

            return session.SessionId;
        }

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

        //public string CreateAccount(User user)
        public string CreateAccount(string sessionId)
        {
            //var userEntity = DAL.Instance.Users.First(usr => usr.UserName == user.UserName);
            var session = DAL.Instance.Sessions.First(session2 => session2.SessionId == sessionId);
            var user = DAL.Instance.Users.First(user2 => user2.Id == session.UserId);
            //DAL.Instance.Accounts.InsertOne(new Account(userEntity.Id));
            var newAccount = new Account { OwnerId = user.Id };
            DAL.Instance.Accounts.Add(newAccount);
            user.Accounts.Add(newAccount.Id);
            DAL.Instance.Users.Update(user);

            return "OK";
        }

        public string DeleteAccount(string accountId)
        {
            var ownerId = DAL.Instance.Accounts.First(account => account.Id == accountId).OwnerId;
            DAL.Instance.Accounts.Delete(accountId);
            var user = DAL.Instance.Users.First(user2 => user2.Id == ownerId);
            user.Accounts.Remove(accountId);
            DAL.Instance.Users.Update(user);

            return "OK";
        }

        public async Task<string> Transfer(Operation operation)
        {
            //var s = DAL.Instance.Configurations.AsQueryable().First(config => config.Key == "CurrentAccountId").Value;
            if (AccountIdAnalyzer.IsInternalAccount(operation.DestinationId))
            {
                TransferAndPaymentManager.ExecuteInternalTransfer(operation);
            }
            else
            {
                await TransferAndPaymentManager.ExecuteExternalTransfer(operation);
            }                  

            //var sourceAccount = DAL.Instance.Accounts.Single(account => account.Id == operation.SourceId);
            //var destinationAccount = DAL.Instance.Accounts.Single(account => account.Id == operation.DestinationId);
            //var operationInDestinationView = operation.Clone();

            //operation.BalanceBefore = sourceAccount.Balance;
            //operationInDestinationView.BalanceBefore = destinationAccount.Balance;

            //sourceAccount.Balance -= operation.Amount;
            //destinationAccount.Balance += operation.Amount;

            //operation.BalanceAfter = sourceAccount.Balance;
            //operationInDestinationView.BalanceAfter = destinationAccount.Balance;

            //DAL.Instance.Operations.Add(operation);
            //DAL.Instance.Operations.Add(operationInDestinationView);

            //sourceAccount.OperationsHistory.Add(operation.Id);
            //destinationAccount.OperationsHistory.Add(operationInDestinationView.Id);

            //DAL.Instance.Accounts.Update(sourceAccount);
            //DAL.Instance.Accounts.Update(destinationAccount);

            return "OK";
        }

        public string Payment(Operation operation)
        {
            if (AccountIdAnalyzer.IsValidId(operation.DestinationId))
            {
                TransferAndPaymentManager.ExecuteGainingOperation(operation);
            }
            else
            {
                TransferAndPaymentManager.ExecuteSpendingOperation(operation);
            }

            return "OK";
        }

        //public HttpResponseMessage ReceiveExternalTransfer(string id, int amount, string from, string title)
        public string ReceiveExternalTransfer(string id, ExternalOperation externalOperation)
        {
            //var reminder = amount % 10;
            //amount /= 10;
            //reminder += 10 * (amount % 10);
            //amount /= 10;

            //var decimalTotalAmount = amount + ((decimal) reminder) / 100;

            //var operation = new Operation
            //{
            //    Amount = decimalTotalAmount,
            //    DateTime = DateTime.Now,
            //    DestinationId = id,
            //    SourceId = from,
            //    Title = title
            //};

            var webContext = WebOperationContext.Current;
            webContext.OutgoingResponse.StatusCode = HttpStatusCode.Created;
            //webContext.IncomingRequest.

            var operation = new Operation(externalOperation, id);  
            
            TransferAndPaymentManager.ExecuteGainingOperation(operation);

            return "OK";
        }      
    }
}
