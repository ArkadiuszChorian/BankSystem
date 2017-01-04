using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Service.Models;

namespace Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IBankService" in both code and config file together.
    [ServiceContract]
    public interface IBankService
    {
        //======= Client - Service (SOAP) 
        [OperationContract]
        List<Account> GetAccounts(string sessionId);
        [OperationContract]
        List<string> GetAccountsIds(string sessionId);
        [OperationContract]
        List<Operation> GetAccountHistory(string accountId);
        [OperationContract]
        string GenerateSessionId(string userName);
        [OperationContract]
        string AuthenticateUser(string userName, string password);
        [OperationContract]
        string RegisterUser(User user);
        [OperationContract]
        string CreateAccount(User user);
        [OperationContract]
        string Transfer(Operation operation);
        [OperationContract]
        string Payment(Operation operation);

        //======= Service - Service (REST) 
        //[OperationContract]
        //[WebInvoke(Method = "POST", UriTemplate = "/accounts/{id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        //string ReceiveExternalTransfer(string id, int amount, string from, string title);
    }
}
