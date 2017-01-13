﻿using System;
using Service.Models;

namespace Service.Analyzers
{
    public class OperationAnalyzer : IValidator<Operation>
    {
        public AccountAnalyzer AccountAnalyzer { get; set; } = new AccountAnalyzer();
        public bool Validate(Operation operation)
        {
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

        public bool HasSufficientBalance(Account account, Operation operation)
        {
            return account.Balance - operation.Amount >= 0;
        }
    }
}