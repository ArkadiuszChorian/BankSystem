using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Service.Analyzers;
using Service.Models;

namespace Service.Managers
{
    public class OperationManager
    {
        //public AccountAnalyzer AccountAnalyzer { get; set; } = new AccountAnalyzer();
        public OperationAnalyzer OperationAnalyzer { get; set; } = new OperationAnalyzer();
        public AuthenticationManager AuthenticationManager { get; set; } = new AuthenticationManager();

        public async void ExecuteOperation(Operation operation)
        {
            OperationAnalyzer.Validate(operation);

            switch (operation.OperationType)
            {
                case Operation.OperationTypes.Transfer:
                    var sourceAccountIsInternal = OperationAnalyzer.AccountAnalyzer.IsInternalAccount(operation.SourceId);
                    var destinationAccountIsInternal = OperationAnalyzer.AccountAnalyzer.IsInternalAccount(operation.DestinationId);

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
                       
                        var responseCode = await ExecuteExternalTransfer(operation, AuthenticationManager.CreateBankCredentials());
                        if (responseCode == HttpStatusCode.Created)
                        {
                            ExecuteOperation(operation.Clone(), sourceAccount, OperationDirections.Expense);
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
                    if (OperationAnalyzer.AccountAnalyzer.IsInternalAccount(operation.DestinationId))
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
                    if (OperationAnalyzer.AccountAnalyzer.IsInternalAccount(operation.SourceId))
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
                    throw new ArgumentOutOfRangeException(nameof(operation.OperationType), operation.OperationType, "Operation type is null or incorrect");
            }
        }
        //public void ExecuteInternalTransfer(Operation operation)
        //{
        //    ExecuteExpenseOperation(operation.Clone());
        //    ExecuteIncomeOperation(operation.Clone());
        //}

        public async Task<HttpStatusCode> ExecuteExternalTransfer(Operation operation, string credentials)
        {
            using (var client = new HttpClient())
            {
                var externalOperation = new ExternalTransfer(operation);

                //TODO Needs mapping from BankId to IP
                //const string pcIp = "192.168.1.11";
                var externalIp = ConfigurationManager.AppSettings["ExternalIp"];
                var myOwnBankBaseAdress = "http://" + externalIp + "/BankService/web";
                var url = myOwnBankBaseAdress + "/accounts/" + operation.DestinationId;

                var content = new StringContent(externalOperation.ToJson(), Encoding.UTF8, "application/json");

                //Todo
                //httpWebRequest.Headers.Add("Authorization", "Basic " + encoded);

                //var json = externalOperation.ToJson();
                //var content = new FormUrlEncodedContent(externalOperation);
                //client.DefaultRequestHeaders.Authorization

                client.DefaultRequestHeaders.Add("Authorization", credentials);
                var response = await client.PostAsync(url, content);

                return response.StatusCode;

                //if (response.StatusCode == HttpStatusCode.Created)
                //{
                //    ExecuteExpenseOperation(operation);
                //}

                //var responseString = await response.Content.ReadAsStringAsync();
            }
        }

        //public void ExecuteIncomeOperation(Operation operation)
        //{
        //    Account destinationAccount;

        //    try
        //    {
        //        destinationAccount = DAL.Instance.Accounts.Single(account => account.Id == operation.DestinationId);
        //    }
        //    catch (InvalidOperationException exception)
        //    {
        //        throw new OperationCanceledException("Destination account does not exist.", exception);
        //    }

        //    ExecuteOperation(operation, destinationAccount, true);
        //}

        //public void ExecuteExpenseOperation(Operation operation)
        //{
        //    Account sourceAccount;

        //    try
        //    {
        //        sourceAccount = DAL.Instance.Accounts.Single(account => account.Id == operation.SourceId);
        //    }
        //    catch (InvalidOperationException exception)
        //    {              
        //        throw new OperationCanceledException("Source account does not exist.", exception);
        //    }
            
        //    ExecuteOperation(operation, sourceAccount, false);
        //}

        private void ExecuteOperation(Operation operation, Account account, OperationDirections direction)
        {
            operation.BalanceBefore = account.Balance;

            if (direction == OperationDirections.Expense && !OperationAnalyzer.HasSufficientBalance(account, operation))
                throw new InvalidOperationException("Account balance is not sufficient to execute operation.");

            account.Balance += (decimal)direction * operation.Amount;

            operation.BalanceAfter = account.Balance;           

            DAL.Instance.Operations.Add(operation);

            account.OperationsHistory.Add(operation.Id);

            DAL.Instance.Accounts.Update(account);
        }

        //private void ExecuteOperation(Operation operation, Account account, bool isIncomeOperation)
        //{
            
        //    operation.BalanceBefore = account.Balance;

        //    if (isIncomeOperation)
        //    {
        //        account.Balance += operation.Amount;
        //    }
        //    else
        //    {
        //        account.Balance -= operation.Amount;
        //    }
            
        //    operation.BalanceAfter = account.Balance;

        //    DAL.Instance.Operations.Add(operation);

        //    account.OperationsHistory.Add(operation.Id);

        //    DAL.Instance.Accounts.Update(account);
        //}

        private enum OperationDirections
        {
            Expense = -1,
            Income = 1
        }
    }
}
