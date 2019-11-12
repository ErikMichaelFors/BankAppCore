using Application.Interfaces;
using Application.Transactions.Commands.CreateTransaction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Models.AccountModels
{
    public class WithdrawalViewModel
    {
        [Required]
        public int Account { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public string StatusMessage { get; private set; }

        public WithdrawalViewModel()
        {
        }

        public bool MakeWithdrawal(IBankAppDataContext context)
        {
            CreateTransaction createTransaction = new CreateTransaction(context);
            if (createTransaction.Withdrawal(Account, Amount))
            {
                StatusMessage = "Withdrawal successful";
                return true;
            }
            StatusMessage = createTransaction.ErrorMessage;
            return false;
        }
    }
}
