using Application.Interfaces;
using Application.Transactions.Commands.CreateTransaction;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Models.AccountModels
{
    public class AccountTransactionViewModel
    {
        private IBankAppDataContext _context { get; set; }
        [Required]
        public int ToAccount { get; private set; }
        [Required]
        public int FromAccount { get; private set; }
        public Transaction Transaction { get; private set; }
        [Required]
        public decimal Amount { get; set; }

        public string StatusMessage { get; private set; }

        public bool Deposit { get; set; }
        public bool Withdrawal { get; set; }
        public bool Transfer { get; set; }

        public AccountTransactionViewModel(IBankAppDataContext context)
        {
            Deposit = false;
            Withdrawal = false;
            Transfer = false;
            _context = context;
        }

        public void SetFromAccount(int from)
        {
            FromAccount = from;
        }

        public void SetToAccount(int to)
        {
            ToAccount = to;
        }

        public bool MakeTransfer()
        {
            Transfer = true;
            CreateTransaction createTransaction = new CreateTransaction(_context);
            if (createTransaction.Transfer(FromAccount, ToAccount, Amount))
            {
                StatusMessage = "Transfer successful";
                return true;
            }
            return false;
        }
        public bool MakeDeposit()
        {
            Deposit = true;
            CreateTransaction createTransaction = new CreateTransaction(_context);
            if (createTransaction.Deposit(ToAccount, Amount))
            {
                StatusMessage = "Deposit successful";
                return true;
            }
            return false;
        }
        public bool MakeWithdrawal()
        {
            Withdrawal = true;
            CreateTransaction createTransaction = new CreateTransaction(_context);
            if (createTransaction.Withdrawal(FromAccount, Amount))
            {
                StatusMessage = "Withdrawal successful";
                return true;
            }
            return false;
        }
    }
}
