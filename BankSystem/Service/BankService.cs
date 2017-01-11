using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using Service.Managers;
using Service.Models;
using Service.Providers;
using AuthenticationManager = Service.Managers.AuthenticationManager;

namespace Service
{
    public class BankService : IBankService, IBankServiceWeb
    {
        public AccountIdAnalyzer AccountIdAnalyzer { get; set; } = new AccountIdAnalyzer();
        public OperationManager OperationManager { get; set; } = new OperationManager();
        public AuthenticationManager AuthenticationManager { get; set; } = new AuthenticationManager();

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

        public string CreateSession(string userName)
        {
            var user = DAL.Instance.Users.First(user2 => user2.UserName == userName);
            var sessionId = AuthenticationManager.CreateSession(user);

            return sessionId;
        }

        public bool AuthenticateUser(string userName, string password)
        {
            try
            {
                var user = DAL.Instance.Users.FirstOrDefault(user2 => user2.UserName == userName);
                if (user?.Password == password)
                {
                    //return "OK";
                }
            }
            catch (InvalidOperationException e)
            {
                //Console.WriteLine(e);
                throw new FaultException(e.Message);
            }

            //return "ERR";
            return true;
        }

        public bool RegisterUser(User user)
        {
            DAL.Instance.Users.Add(user);

            return true;
        }

        //public string CreateAccount(User user)
        public bool CreateAccount(string sessionId)
        {
            //var userEntity = DAL.Instance.Users.First(usr => usr.UserName == user.UserName);
            var session = DAL.Instance.Sessions.First(session2 => session2.SessionId == sessionId);
            var user = DAL.Instance.Users.First(user2 => user2.Id == session.UserId);
            //DAL.Instance.Accounts.InsertOne(new Account(userEntity.Id));
            var newAccount = new Account {OwnerId = user.Id};
            DAL.Instance.Accounts.Add(newAccount);
            user.Accounts.Add(newAccount.Id);
            DAL.Instance.Users.Update(user);

            return true;
        }

        public bool DeleteAccount(string accountId)
        {
            var ownerId = DAL.Instance.Accounts.First(account => account.Id == accountId).OwnerId;
            DAL.Instance.Accounts.Delete(accountId);
            var user = DAL.Instance.Users.First(user2 => user2.Id == ownerId);
            user.Accounts.Remove(accountId);
            DAL.Instance.Users.Update(user);

            return true;
        }

        public async Task<bool> Transfer(Operation operation)
        {
            //var s = DAL.Instance.Configurations.AsQueryable().First(config => config.Key == "CurrentAccountId").Value;
            if (AccountIdAnalyzer.IsInternalAccount(operation.DestinationId))
            {
                OperationManager.ExecuteInternalTransfer(operation);
            }
            else
            {
                var credentials = AuthenticationManager.CreateBankCredentials();
                await OperationManager.ExecuteExternalTransfer(operation, credentials);
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

            return true;
        }

        public bool Payment(Operation operation)
        {
            if (AccountIdAnalyzer.IsValidId(operation.DestinationId))
            {
                OperationManager.ExecuteIncomeOperation(operation);
            }
            else
            {
                OperationManager.ExecuteExpenseOperation(operation);
            }

            return true;
        }
        
        public string ReceiveExternalTransfer(string id, ExternalOperation externalOperation)
        {
            var webContext = WebOperationContext.Current;

            try
            {                
                var incomingCredentials = webContext.IncomingRequest.Headers[HttpRequestHeader.Authorization];

                if (AuthenticationManager.CheckBankCredentials(incomingCredentials) == false)
                {
                    webContext.OutgoingResponse.Headers[HttpResponseHeader.WwwAuthenticate] = "Basic realm=\"Base64(login:password)\"";

                    throw new WebFaultException<JsonError>(new JsonError("Incorrect credentials. Check login, password or investigate encoding process."), HttpStatusCode.Unauthorized);
                }

                var operation = new Operation(externalOperation, id);
                OperationManager.ExecuteIncomeOperation(operation);

                webContext.OutgoingResponse.StatusCode = HttpStatusCode.Created;
            }
            catch (FormatException exception)
            {               
                throw new WebFaultException<JsonError>(new JsonError(exception.Message), HttpStatusCode.BadRequest);
            }
            
            return "Success";
        }      
    }
}
