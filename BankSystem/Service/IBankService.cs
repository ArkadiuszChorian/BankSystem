using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using Service.Models;

namespace Service
{
    /// <summary>
    /// Service interface for SOAP API
    /// </summary>
    [ServiceContract]
    public interface IBankService
    {
        [OperationContract]
        List<Account> GetAccounts(string sessionId);
        [OperationContract]
        List<string> GetAccountsIds(string sessionId);
        [OperationContract]
        List<Operation> GetAccountHistory(string accountId);
        [OperationContract]
        string CreateSession(string userName);
        [OperationContract]
        bool AuthenticateUser(string userName, string password);
        [OperationContract]
        bool RegisterUser(User user);
        [OperationContract]
        bool DeleteUser(string sessionId);
        [OperationContract]
        bool CreateAccount(string sessionId);
        [OperationContract]
        bool DeleteAccount(string accountId);

        [OperationContract]
        Task<bool> ExecuteOperation(Operation operation);       
    }
}
