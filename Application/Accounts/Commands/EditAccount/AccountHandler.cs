using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Accounts.Commands.EditAccount
{
    public class AccountHandler
    {
        public string ErrorMessage { get; private set; }

        /*--Exception Messages--*/
        public const string DebitAmountExceedsBalanceMessage = "Amount exceeds the balance of the account";
        public const string DebitAmountIsLessThanZeroMessage = "Amount is less than zero";
        public const string CreditAmountIsLessThanZeroMessage = "Amount is less than zero";
        /*----------------------*/


        public bool IsWidthdrawalAllowed(int accountId, decimal amount, IBankAppDataContext context)
        {
            Account account = context.Accounts.Where(a => a.AccountId == accountId).SingleOrDefault();
            if (account != null)
            {
                if (amount > account.Balance)
                {
                    ErrorMessage = DebitAmountExceedsBalanceMessage;
                    return false;
                }
                else if (amount < 0)
                {
                    ErrorMessage = DebitAmountIsLessThanZeroMessage;
                    return false;
                }
                return true;
            }
            else
            {
                ErrorMessage += "; Account ' doesn't exist";
                return false;
            }
        }

        public bool AreDifferentAccounts(int accountFrom, int accountTo, IBankAppDataContext context)
        {
            Account from = context.Accounts.Where(a => a.AccountId == accountFrom).SingleOrDefault();
            Account to = context.Accounts.Where(a => a.AccountId == accountTo).SingleOrDefault();
            if (from != null && to != null)
            {
                if (from.AccountId != to.AccountId)
                {
                    return true;
                }
                else if (from.AccountId == to.AccountId)
                {
                    ErrorMessage += "; Accounts are the same";
                    return false;
                }
                return false;
            }
            else if (from == null)
            {
                ErrorMessage += "; Account '" + accountFrom + "' doesn't exsist";
                return false;
            }
            else if (to == null)
            {
                ErrorMessage += "; Account '" + accountTo + "' doesn't exsist";
                return false;
            }
            else return false;
        }

        public bool Deposit(int accountId, decimal amount, IBankAppDataContext context)
        {
            Account account = context.Accounts.Where(a => a.AccountId == accountId).SingleOrDefault();
            if (account != null)
            {
                if (amount < 0)
                {
                    ErrorMessage = CreditAmountIsLessThanZeroMessage;
                    return false;
                }
                else if (amount > 0)
                {
                    account.Balance += amount;
                    context.Save();
                    return true;
                }
            }
            return false;
        }

        public void Withdrawal(int accountId, decimal amount, IBankAppDataContext context)
        {
            Account account = context.Accounts.Where(a => a.AccountId == accountId).SingleOrDefault();
            if (IsWidthdrawalAllowed(accountId, amount, context))
            {
                account.Balance -= amount;
                context.Save();
            }
        }
    }
}
