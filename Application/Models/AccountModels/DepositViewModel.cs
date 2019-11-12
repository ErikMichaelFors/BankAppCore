using Application.Interfaces;
using Application.Transactions.Commands.CreateTransaction;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Models.AccountModels
{
    public class DepositViewModel
    {
        [Required]
        public int Account { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public string StatusMessage { get; private set; }

        public DepositViewModel()
        {
        }

        public bool MakeDeposit(IBankAppDataContext context)
        {
            CreateTransaction createTransaction = new CreateTransaction(context);
            if (createTransaction.Deposit(Account, Amount))
            {
                StatusMessage = "Deposit successful";
                return true;
            }
            StatusMessage = createTransaction.ErrorMessage;
            return false;
        }
    }
}
