using Application.Accounts.Commands.EditAccount;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Domain.Entities;
using System.Linq;

namespace Application.Transactions.Commands.CreateTransaction
{
    public class CreateTransaction
    {
        private IBankAppDataContext _context { get; set; }
        private AccountHandler accountHandler { get; set; }
        public string ErrorMessage { get; private set; }

        public CreateTransaction(IBankAppDataContext context)
        {
            _context = context;
            accountHandler = new AccountHandler();
        }

        public bool Transfer(int fromAccount, int toAccount, decimal amount)
        {
            if (accountHandler.AreDifferentAccounts(fromAccount, toAccount, _context))
            {
                if (accountHandler.IsWidthdrawalAllowed(fromAccount, amount, _context))
                {
                    accountHandler.Withdrawal(fromAccount, amount, _context);
                    accountHandler.Deposit(toAccount, amount, _context);

                    DateTime now = DateTime.Now;

                    Domain.Entities.Transaction fromTransaction = new Domain.Entities.Transaction();
                    fromTransaction.AccountId = fromAccount;
                    fromTransaction.Type = "Debit";
                    fromTransaction.Operation = "Transfer to account " + toAccount;
                    fromTransaction.Amount = amount;
                    fromTransaction.Date = now;
                    fromTransaction.Balance = _context.Accounts.Where(a => a.AccountId == fromAccount).SingleOrDefault().Balance;
                    fromTransaction.Account = toAccount.ToString();
                    _context.Transactions.Add(fromTransaction);

                    Domain.Entities.Transaction toTransaction = new Domain.Entities.Transaction();
                    toTransaction.AccountId = toAccount;
                    toTransaction.Type = "Credit";
                    toTransaction.Operation = "Transfer from account " + fromAccount;
                    toTransaction.Amount = amount;
                    toTransaction.Date = now;
                    toTransaction.Balance = _context.Accounts.Where(a => a.AccountId == toAccount).SingleOrDefault().Balance;
                    toTransaction.Account = toAccount.ToString();
                    _context.Transactions.Add(toTransaction);

                    _context.Save();
                    return true;
                }
                ErrorMessage = accountHandler.ErrorMessage;
            }
            return false;
        }

        public bool Deposit(int toAccount, decimal amount)
        {
            if (accountHandler.Deposit(toAccount, amount, _context))
            {
                Domain.Entities.Transaction toTransaction = new Domain.Entities.Transaction();
                toTransaction.AccountId = toAccount;
                toTransaction.Type = "Credit";
                toTransaction.Operation = "Deposit";
                toTransaction.Amount = amount;
                toTransaction.Date = DateTime.Now;
                toTransaction.Balance = _context.Accounts.Where(a => a.AccountId == toAccount).SingleOrDefault().Balance;
                _context.Transactions.Add(toTransaction);
                _context.Save();
                return true;
            }
            ErrorMessage = accountHandler.ErrorMessage;
            return false;
        }
        public bool Withdrawal(int fromAccount, decimal amount)
        {
            if (accountHandler.IsWidthdrawalAllowed(fromAccount, amount, _context))
            {
                accountHandler.Withdrawal(fromAccount, amount, _context);

                Domain.Entities.Transaction fromTransaction = new Domain.Entities.Transaction();
                fromTransaction.AccountId = fromAccount;
                fromTransaction.Type = "Debit";
                fromTransaction.Operation = "Withdrawal";
                fromTransaction.Amount = amount;
                fromTransaction.Date = DateTime.Now;
                fromTransaction.Balance = _context.Accounts.Where(a => a.AccountId == fromAccount).SingleOrDefault().Balance;
                _context.Transactions.Add(fromTransaction);
                _context.Save();
                return true;
            }
            ErrorMessage = accountHandler.ErrorMessage;
            return false;
        }
    }
}
