﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     //
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BankService
{
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "0.4.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Account", Namespace="http://schemas.datacontract.org/2004/07/Service.Models")]
    public partial class Account : object
    {
        
        private decimal BalanceField;
        
        private string IdField;
        
        private System.Collections.Generic.List<string> OperationsHistoryField;
        
        private string OwnerIdField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Balance
        {
            get
            {
                return this.BalanceField;
            }
            set
            {
                this.BalanceField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Id
        {
            get
            {
                return this.IdField;
            }
            set
            {
                this.IdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.List<string> OperationsHistory
        {
            get
            {
                return this.OperationsHistoryField;
            }
            set
            {
                this.OperationsHistoryField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string OwnerId
        {
            get
            {
                return this.OwnerIdField;
            }
            set
            {
                this.OwnerIdField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "0.4.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Operation", Namespace="http://schemas.datacontract.org/2004/07/Service.Models")]
    public partial class Operation : object
    {
        
        private decimal AmountField;
        
        private decimal BalanceAfterField;
        
        private decimal BalanceBeforeField;
        
        private System.DateTime DateTimeField;
        
        private string DestinationIdField;
        
        private string IdField;
        
        private BankService.Operation.OperationTypes OperationTypeField;
        
        private string SourceIdField;
        
        private string TitleField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Amount
        {
            get
            {
                return this.AmountField;
            }
            set
            {
                this.AmountField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal BalanceAfter
        {
            get
            {
                return this.BalanceAfterField;
            }
            set
            {
                this.BalanceAfterField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal BalanceBefore
        {
            get
            {
                return this.BalanceBeforeField;
            }
            set
            {
                this.BalanceBeforeField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime DateTime
        {
            get
            {
                return this.DateTimeField;
            }
            set
            {
                this.DateTimeField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DestinationId
        {
            get
            {
                return this.DestinationIdField;
            }
            set
            {
                this.DestinationIdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Id
        {
            get
            {
                return this.IdField;
            }
            set
            {
                this.IdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public BankService.Operation.OperationTypes OperationType
        {
            get
            {
                return this.OperationTypeField;
            }
            set
            {
                this.OperationTypeField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SourceId
        {
            get
            {
                return this.SourceIdField;
            }
            set
            {
                this.SourceIdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Title
        {
            get
            {
                return this.TitleField;
            }
            set
            {
                this.TitleField = value;
            }
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "0.4.0.0")]
        [System.Runtime.Serialization.DataContractAttribute(Name="Operation.OperationTypes", Namespace="http://schemas.datacontract.org/2004/07/Service.Models")]
        public enum OperationTypes : int
        {
            
            [System.Runtime.Serialization.EnumMemberAttribute()]
            Transfer = 0,
            
            [System.Runtime.Serialization.EnumMemberAttribute()]
            Payment = 1,
            
            [System.Runtime.Serialization.EnumMemberAttribute()]
            Withdraw = 2,
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "0.4.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="User", Namespace="http://schemas.datacontract.org/2004/07/Service.Models")]
    public partial class User : object
    {
        
        private System.Collections.Generic.List<string> AccountsField;
        
        private string IdField;
        
        private string PasswordField;
        
        private System.Collections.Generic.List<string> SessionsField;
        
        private string UserNameField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.List<string> Accounts
        {
            get
            {
                return this.AccountsField;
            }
            set
            {
                this.AccountsField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Id
        {
            get
            {
                return this.IdField;
            }
            set
            {
                this.IdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Password
        {
            get
            {
                return this.PasswordField;
            }
            set
            {
                this.PasswordField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.List<string> Sessions
        {
            get
            {
                return this.SessionsField;
            }
            set
            {
                this.SessionsField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserName
        {
            get
            {
                return this.UserNameField;
            }
            set
            {
                this.UserNameField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "0.4.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ExternalTransfer", Namespace="http://schemas.datacontract.org/2004/07/Service.Models")]
    public partial class ExternalTransfer : object
    {
        
        private int amountField;
        
        private string fromField;
        
        private string titleField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int amount
        {
            get
            {
                return this.amountField;
            }
            set
            {
                this.amountField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string from
        {
            get
            {
                return this.fromField;
            }
            set
            {
                this.fromField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string title
        {
            get
            {
                return this.titleField;
            }
            set
            {
                this.titleField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "0.4.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="BankService.IBankService")]
    public interface IBankService
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBankService/GetAccounts", ReplyAction="http://tempuri.org/IBankService/GetAccountsResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<BankService.Account>> GetAccountsAsync(string sessionId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBankService/GetAccountsIds", ReplyAction="http://tempuri.org/IBankService/GetAccountsIdsResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<string>> GetAccountsIdsAsync(string sessionId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBankService/GetAccountHistory", ReplyAction="http://tempuri.org/IBankService/GetAccountHistoryResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<BankService.Operation>> GetAccountHistoryAsync(string accountId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBankService/CreateSession", ReplyAction="http://tempuri.org/IBankService/CreateSessionResponse")]
        System.Threading.Tasks.Task<string> CreateSessionAsync(string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBankService/AuthenticateUser", ReplyAction="http://tempuri.org/IBankService/AuthenticateUserResponse")]
        System.Threading.Tasks.Task<bool> AuthenticateUserAsync(string userName, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBankService/RegisterUser", ReplyAction="http://tempuri.org/IBankService/RegisterUserResponse")]
        System.Threading.Tasks.Task<bool> RegisterUserAsync(BankService.User user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBankService/DeleteUser", ReplyAction="http://tempuri.org/IBankService/DeleteUserResponse")]
        System.Threading.Tasks.Task<bool> DeleteUserAsync(string sessionId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBankService/CreateAccount", ReplyAction="http://tempuri.org/IBankService/CreateAccountResponse")]
        System.Threading.Tasks.Task<bool> CreateAccountAsync(string sessionId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBankService/DeleteAccount", ReplyAction="http://tempuri.org/IBankService/DeleteAccountResponse")]
        System.Threading.Tasks.Task<bool> DeleteAccountAsync(string accountId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBankService/ExecuteOperation", ReplyAction="http://tempuri.org/IBankService/ExecuteOperationResponse")]
        System.Threading.Tasks.Task<bool> ExecuteOperationAsync(BankService.Operation operation);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "0.4.0.0")]
    public interface IBankServiceChannel : BankService.IBankService, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "0.4.0.0")]
    public partial class BankServiceClient : System.ServiceModel.ClientBase<BankService.IBankService>, BankService.IBankService
    {
        
    /// <summary>
    /// Implement this partial method to configure the service endpoint.
    /// </summary>
    /// <param name="serviceEndpoint">The endpoint to configure</param>
    /// <param name="clientCredentials">The client credentials</param>
    static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public BankServiceClient() : 
                base(BankServiceClient.GetDefaultBinding(), BankServiceClient.GetDefaultEndpointAddress())
        {
            this.Endpoint.Name = EndpointConfiguration.BasicHttpBinding_IBankService.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public BankServiceClient(EndpointConfiguration endpointConfiguration) : 
                base(BankServiceClient.GetBindingForEndpoint(endpointConfiguration), BankServiceClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public BankServiceClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(BankServiceClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public BankServiceClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(BankServiceClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public BankServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<BankService.Account>> GetAccountsAsync(string sessionId)
        {
            return base.Channel.GetAccountsAsync(sessionId);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<string>> GetAccountsIdsAsync(string sessionId)
        {
            return base.Channel.GetAccountsIdsAsync(sessionId);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<BankService.Operation>> GetAccountHistoryAsync(string accountId)
        {
            return base.Channel.GetAccountHistoryAsync(accountId);
        }
        
        public System.Threading.Tasks.Task<string> CreateSessionAsync(string userName)
        {
            return base.Channel.CreateSessionAsync(userName);
        }
        
        public System.Threading.Tasks.Task<bool> AuthenticateUserAsync(string userName, string password)
        {
            return base.Channel.AuthenticateUserAsync(userName, password);
        }
        
        public System.Threading.Tasks.Task<bool> RegisterUserAsync(BankService.User user)
        {
            return base.Channel.RegisterUserAsync(user);
        }
        
        public System.Threading.Tasks.Task<bool> DeleteUserAsync(string sessionId)
        {
            return base.Channel.DeleteUserAsync(sessionId);
        }
        
        public System.Threading.Tasks.Task<bool> CreateAccountAsync(string sessionId)
        {
            return base.Channel.CreateAccountAsync(sessionId);
        }
        
        public System.Threading.Tasks.Task<bool> DeleteAccountAsync(string accountId)
        {
            return base.Channel.DeleteAccountAsync(accountId);
        }
        
        public System.Threading.Tasks.Task<bool> ExecuteOperationAsync(BankService.Operation operation)
        {
            return base.Channel.ExecuteOperationAsync(operation);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_IBankService))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_IBankService))
            {
                return new System.ServiceModel.EndpointAddress("http://localhost:8733/service");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding()
        {
            return BankServiceClient.GetBindingForEndpoint(EndpointConfiguration.BasicHttpBinding_IBankService);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress()
        {
            return BankServiceClient.GetEndpointAddress(EndpointConfiguration.BasicHttpBinding_IBankService);
        }
        
        public enum EndpointConfiguration
        {
            
            BasicHttpBinding_IBankService,
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "0.4.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="BankService.IBankServiceWeb")]
    public interface IBankServiceWeb
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBankServiceWeb/ReceiveExternalTransfer", ReplyAction="http://tempuri.org/IBankServiceWeb/ReceiveExternalTransferResponse")]
        System.Threading.Tasks.Task<string> ReceiveExternalTransferAsync(string id, BankService.ExternalTransfer externalTransfer);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "0.4.0.0")]
    public interface IBankServiceWebChannel : BankService.IBankServiceWeb, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "0.4.0.0")]
    public partial class BankServiceWebClient : System.ServiceModel.ClientBase<BankService.IBankServiceWeb>, BankService.IBankServiceWeb
    {
        
        public BankServiceWebClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        public System.Threading.Tasks.Task<string> ReceiveExternalTransferAsync(string id, BankService.ExternalTransfer externalTransfer)
        {
            return base.Channel.ReceiveExternalTransferAsync(id, externalTransfer);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
    }
}
