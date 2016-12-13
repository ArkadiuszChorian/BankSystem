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
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        //======= Client - Service (SOAP) 
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
        string RetriveTransfer(Operation operation);
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "Service.ContractType".
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
