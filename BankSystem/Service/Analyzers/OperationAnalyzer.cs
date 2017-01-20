using System;
using Service.Models;

namespace Service.Analyzers
{
    /// <summary>
    /// Class for analyzing operations
    /// </summary>
    public class OperationAnalyzer : IValidator<Operation>
    {
        /// <summary>
        /// Gets or sets account analyzer
        /// </summary>
        public AccountAnalyzer AccountAnalyzer { get; set; } = new AccountAnalyzer();
        /// <summary>
        /// Validates operation
        /// </summary>
        /// <param name="operation"></param>
        /// <returns></returns>
        public bool Validate(Operation operation)
        {
            if (operation.DestinationId == operation.SourceId)
            {
                throw new ArgumentException("Cannot transfer to yourself.");
            }

            switch (operation.OperationType)
            {
                case Operation.OperationTypes.Transfer:
                    AccountAnalyzer.ValidateId(operation.SourceId, true);
                    AccountAnalyzer.ValidateId(operation.DestinationId, true);
                    break;
                case Operation.OperationTypes.Payment:
                    AccountAnalyzer.ValidateId(operation.DestinationId, true);
                    break;
                case Operation.OperationTypes.Withdraw:
                    AccountAnalyzer.ValidateId(operation.SourceId, true);
                    break;
                default:
                    throw new ArgumentException("Operation type is null or incorrect");
            }  
            if (operation.Amount <= 0)
            {
                throw new ArgumentException("Operation amount is equal or below zero.");
            }

            return true;
        }
        /// <summary>
        /// Checks if given account has sufficent balance for given operation
        /// </summary>
        /// <param name="account"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        public bool HasSufficientBalance(Account account, Operation operation)
        {
            return account.Balance - operation.Amount >= 0;
        }
    }
}
