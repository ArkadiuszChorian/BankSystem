using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Authentication;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;
using Service.Managers;
using Service.Models;
using AuthenticationManager = Service.Managers.AuthenticationManager;

namespace Service
{
    public class BankService : IBankService, IBankServiceWeb
    {
        private OperationManager _operationManager = new OperationManager();
        private AuthenticationManager _authenticationManager = new AuthenticationManager();
        private AccountManager _accountManager = new AccountManager();
        private UserManager _userManager = new UserManager();

        /// <summary>
        /// Gets accounts for specific session id
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns>Accounts list</returns>
        public List<Account> GetAccounts(string sessionId)
        {
            var user = _authenticationManager.GetUserFromSessionId(sessionId);
            var accounts = _accountManager.GetUserAccounts(user);

            return accounts;
        }

        /// <summary>
        /// Gets accounts ids for specific session id
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns>Accounts ids list</returns>
        public List<string> GetAccountsIds(string sessionId)
        {
            var user = _authenticationManager.GetUserFromSessionId(sessionId);

            return user.Accounts;
        }

        /// <summary>
        /// Gets account historical operations for specific account id
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns>Operations list</returns>
        public List<Operation> GetAccountHistory(string accountId)
        {
            var operations = _accountManager.GetAccountHistory(accountId);

            return operations;
        }

        /// <summary>
        /// Creates new session for specific user name
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>Session id</returns>
        public string CreateSession(string userName)
        {
            var sessionId = _authenticationManager.CreateSession(userName);

            return sessionId;
        }

        /// <summary>
        /// Checks user credentials
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns>Bool representing if user is authenticated</returns>
        public bool AuthenticateUser(string userName, string password)
        {
            try
            {
                _authenticationManager.ChceckUserCredentials(userName, password);
            }
            catch (AuthenticationException exception)
            {
                throw new FaultException(exception.Message);
            }
            
            return true;
        }

        /// <summary>
        /// Registers given user
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Bool representing if user has been registered</returns>
        public bool RegisterUser(User user)
        {          
            _userManager.RegisterUser(user);

            return true;
        }

        /// <summary>
        /// Deletes user by his session id
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns>Bool representing if user has been deleted</returns>
        public bool DeleteUser(string sessionId)
        {
            var user = _authenticationManager.GetUserFromSessionId(sessionId);
            _userManager.DeleteUser(user);

            return true;
        }

        /// <summary>
        /// Creates new user account by his session id
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns>Bool representing if account has been created</returns>
        public bool CreateAccount(string sessionId)
        {
            var user = _authenticationManager.GetUserFromSessionId(sessionId);
            _accountManager.CreateAccount(user);

            return true;
        }

        /// <summary>
        /// Deletes account by account id
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns>Bool representing if account has been deleted</returns>
        public bool DeleteAccount(string accountId)
        {
            _accountManager.DeleteAccount(accountId);

            return true;
        }

        /// <summary>
        /// Executes given operation
        /// </summary>
        /// <param name="operation"></param>
        /// <returns>Bool representing if operation has been executed</returns>
        public async Task<bool> ExecuteOperation(Operation operation)
        {
            try
            {
                await _operationManager.ExecuteOperation(operation);
            }
            catch (Exception exception)
            {
               throw new FaultException(exception.Message);
            }
            
            return true;
        }
        
        /// <summary>
        /// Tries to receive external transfer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="externalTransfer"></param>
        /// <returns>String representing succesful receiving</returns>
        public async Task<string> ReceiveExternalTransfer(string id, ExternalTransfer externalTransfer)
        {
            var webContext = WebOperationContext.Current;
            
            try
            {                
                var incomingCredentials = webContext.IncomingRequest.Headers[HttpRequestHeader.Authorization];

                if (_authenticationManager.CheckBankCredentials(incomingCredentials) == false)
                {
                    webContext.OutgoingResponse.Headers[HttpResponseHeader.WwwAuthenticate] = "Basic realm=\"Base64(login:password)\"";

                    throw new WebFaultException<JsonError>(new JsonError("Incorrect credentials. Check login, password or investigate encoding process."), HttpStatusCode.Unauthorized);
                }

                var operation = new Operation(externalTransfer, id);
                await _operationManager.ExecuteOperation(operation);

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
