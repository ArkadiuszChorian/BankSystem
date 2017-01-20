using System;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using Service.Analyzers;
using Service.Models;

namespace Service.Managers
{
    /// <summary>
    /// Class for managing operations
    /// </summary>
    public class OperationManager
    {
        private OperationAnalyzer _operationAnalyzer = new OperationAnalyzer();
        private AuthenticationManager _authenticationManager = new AuthenticationManager();

        /// <summary>
        /// Executes given operation
        /// </summary>
        /// <param name="operation"></param>
        /// <returns></returns>
        public async Task ExecuteOperation(Operation operation)
        {
            _operationAnalyzer.Validate(operation);

            switch (operation.OperationType)
            {
                case Operation.OperationTypes.Transfer:
                    var sourceAccountIsInternal = _operationAnalyzer.AccountAnalyzer.IsInternalAccount(operation.SourceId);
                    var destinationAccountIsInternal = _operationAnalyzer.AccountAnalyzer.IsInternalAccount(operation.DestinationId);

                    if (sourceAccountIsInternal && destinationAccountIsInternal)
                    {
                        var destinationAccount = DAL.Instance.Accounts.GetDestinationAccount(operation.DestinationId);
                        var sourceAccount = DAL.Instance.Accounts.GetSourceAccount(operation.SourceId);
                        
                        ExecuteOperation(operation.Clone(), sourceAccount, OperationDirections.Expense);
                        ExecuteOperation(operation.Clone(), destinationAccount, OperationDirections.Income);
                    }
                    else if (sourceAccountIsInternal)
                    {
                        var sourceAccount = DAL.Instance.Accounts.GetSourceAccount(operation.SourceId);

                        var responseCode = await ExecuteExternalTransfer(operation, _authenticationManager.CreateBankCredentials());
                        if (responseCode == HttpStatusCode.Created)
                        {
                            ExecuteOperation(operation.Clone(), sourceAccount, OperationDirections.Expense);
                        }
                        else
                        {
                            throw new AuthenticationException("Operation has not been executed because of bank problem.");
                        }
                    }
                    else if (destinationAccountIsInternal)
                    {
                        var destinationAccount = DAL.Instance.Accounts.GetDestinationAccount(operation.DestinationId);
                        
                        ExecuteOperation(operation.Clone(), destinationAccount, OperationDirections.Income);
                    }
                    else
                    {
                        throw new InvalidOperationException("Both source and destination accounts does not exists.");
                    }
                    break;
                case Operation.OperationTypes.Payment:
                    if (_operationAnalyzer.AccountAnalyzer.IsInternalAccount(operation.DestinationId))
                    {
                        var destinationAccount = DAL.Instance.Accounts.GetDestinationAccount(operation.DestinationId);

                        ExecuteOperation(operation.Clone(), destinationAccount, OperationDirections.Income);
                    }
                    else
                    {
                        throw new InvalidOperationException("Both source and destination accounts does not exists.");
                    }
                    break;
                case Operation.OperationTypes.Withdraw:
                    if (_operationAnalyzer.AccountAnalyzer.IsInternalAccount(operation.SourceId))
                    {
                        var sourceAccount = DAL.Instance.Accounts.GetSourceAccount(operation.SourceId);

                        ExecuteOperation(operation.Clone(), sourceAccount, OperationDirections.Expense);
                    }
                    else
                    {
                        throw new InvalidOperationException("Both source and destination accounts does not exists.");
                    }
                    break;
                default:
                    throw new ArgumentException("Operation type is null or incorrect");
            }
        }

        private async Task<HttpStatusCode> ExecuteExternalTransfer(Operation operation, string credentials)
        {
            using (var client = new HttpClient())
            {
                var externalOperation = new ExternalTransfer(operation);
                
                var destinationBankId = _operationAnalyzer.AccountAnalyzer.GetBankIdFromAccountId(operation.DestinationId);
                var externalAddress = DAL.Instance.BankIdToIpMapping[destinationBankId];
                var url = externalAddress + "/accounts/" + operation.DestinationId;

                var content = new StringContent(externalOperation.ToJson(), Encoding.UTF8, "application/json");

                client.DefaultRequestHeaders.Add("Authorization", credentials);
                var response = await client.PostAsync(url, content);

                return response.StatusCode;
            }
        }
      
        private void ExecuteOperation(Operation operation, Account account, OperationDirections direction)
        {
            operation.BalanceBefore = account.Balance;

            if (direction == OperationDirections.Expense && !_operationAnalyzer.HasSufficientBalance(account, operation))
                throw new InvalidOperationException("Account balance is not sufficient to execute operation.");

            account.Balance += (decimal)direction * operation.Amount;

            operation.BalanceAfter = account.Balance; 
            
            operation.DateTime = DateTime.Now;          

            DAL.Instance.Operations.Add(operation);

            account.OperationsHistory.Add(operation.Id);

            DAL.Instance.Accounts.Update(account);
        }
                
        private enum OperationDirections
        {
            Expense = -1,
            Income = 1
        }
    }
}
