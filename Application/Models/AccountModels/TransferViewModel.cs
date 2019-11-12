using Application.Interfaces;
using Application.Transactions.Commands.CreateTransaction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Models.AccountModels
{
    public class TransferViewModel
    {
        [Required]
        public int FromAccount { get; set; }
        [Required]
        public int ToAccount { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public string StatusMessage { get; private set; }

        public TransferViewModel()
        {
        }

        public bool MakeTransfer(IBankAppDataContext context)
        {
            CreateTransaction createTransaction = new CreateTransaction(context);
            if (createTransaction.Transfer(FromAccount, ToAccount, Amount))
            {
                StatusMessage = "Transfer successful";
                return true;
            }
            StatusMessage = createTransaction.ErrorMessage;
            return false;
        }
    }
}
