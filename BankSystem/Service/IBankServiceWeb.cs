using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;
using Service.Models;

namespace Service
{
    /// <summary>
    /// Service interface for REST API
    /// </summary>
    [ServiceContract]
    public interface IBankServiceWeb
    {      
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/accounts/{id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedResponse)]
        Task<string> ReceiveExternalTransfer(string id, ExternalTransfer externalTransfer);
    }
}
