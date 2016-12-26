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
        string AuthenticateUser(string userName, string password);
        [OperationContract]
        string CreateUser(User user);
        [OperationContract]
        string CreateAccount(User user);
        [OperationContract]
        string Transfer(Operation operation);
        [OperationContract]
        string Payment(Operation operation);

        //======= Service - Service (REST) 
        [OperationContract]
        [WebGet(UriTemplate = "/transfer", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string RetrieveTransfer(Operation operation);
    }
}
