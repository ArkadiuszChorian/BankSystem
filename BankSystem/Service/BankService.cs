using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;
using Service.Analyzers;
using Service.Managers;
using Service.Models;
using AuthenticationManager = Service.Managers.AuthenticationManager;

namespace Service
{
    public class BankService : IBankService, IBankServiceWeb
    {
        public OperationManager OperationManager { get; set; } = new OperationManager();
        public AuthenticationManager AuthenticationManager { get; set; } = new AuthenticationManager();
        public AccountManager AccountManager { get; set; } = new AccountManager();
        public UserManager UserManager { get; set; } = new UserManager();

        public List<Account> GetAccounts(string sessionId)
        {
            var user = AuthenticationManager.GetUserFromSessionId(sessionId);
            var accounts = AccountManager.GetUserAccounts(user);

            return accounts;
        }

        public List<string> GetAccountsIds(string sessionId)
        {
            var user = AuthenticationManager.GetUserFromSessionId(sessionId);

            return user.Accounts;
        }

        public List<Operation> GetAccountHistory(string accountId)
        {
            var operations = AccountManager.GetAccountHistory(accountId);

            return operations;
        }

        public string CreateSession(string userName)
        {
            var sessionId = AuthenticationManager.CreateSession(userName);

            return sessionId;
        }

        public bool AuthenticateUser(string userName, string password)
        {
            try
            {
                AuthenticationManager.ChceckUserCredentials(userName, password);
            }
            catch (AuthenticationException exception)
            {
                throw new FaultException(exception.Message);
            }
            
            return true;
        }

        public bool RegisterUser(User user)
        {          
            UserManager.RegisterUser(user);

            return true;
        }

        public bool DeleteUser(string sessionId)
        {
            var user = AuthenticationManager.GetUserFromSessionId(sessionId);
            UserManager.DeleteUser(user);

            return true;
        }

        //public string CreateAccount(User user)
        public bool CreateAccount(string sessionId)
        {
            var user = AuthenticationManager.GetUserFromSessionId(sessionId);
            AccountManager.CreateAccount(user);

            return true;
        }

        public bool DeleteAccount(string accountId)
        {
            AccountManager.DeleteAccount(accountId);

            return true;
        }

        public async Task<bool> ExecuteOperation(Operation operation)
        {
            try
            {
                await OperationManager.ExecuteOperation(operation);
            }
            catch (Exception exception)
            {
               throw new FaultException(exception.Message);
            }
            
            return true;
        }

        //public async Task<bool> Transfer(Operation operation)
        //{
        //    try
        //    {
        //        if (operation.Amount <= 0)
        //        {
        //            throw new OperationCanceledException("Operation amount is equal or below zero.");
        //        }
        //        if (AccountAnalyzer.IsInternalAccount(operation.DestinationId))
        //        {
        //            OperationManager.ExecuteInternalTransfer(operation);
        //        }
        //        else
        //        {
        //            var credentials = AuthenticationManager.CreateBankCredentials();
        //            await OperationManager.ExecuteExternalTransfer(operation, credentials);
        //        }
        //    }
        //    catch (OperationCanceledException exception)
        //    {
        //        throw new FaultException(exception.Message);
        //    }

        //    return true;
        //}

        //public bool Payment(Operation operation)
        //{
        //    if (AccountAnalyzer.Validate(operation.DestinationId))
        //    {
        //        OperationManager.ExecuteIncomeOperation(operation);
        //    }
        //    else
        //    {
        //        OperationManager.ExecuteExpenseOperation(operation);
        //    }

        //    return true;
        //}
        
        public async Task<string> ReceiveExternalTransfer(string id, ExternalTransfer externalTransfer)
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

                var operation = new Operation(externalTransfer, id);
                await OperationManager.ExecuteOperation(operation);

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
