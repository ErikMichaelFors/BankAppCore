using Application.Accounts.Commands.EditAccount;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Persistence;
using System;

namespace MSBankAppTests
{
    [TestClass]
    public class DebitAndCreditTests
    {/*
        [TestMethod]
        public void Withdrawal_CantWithdrawMoreThanBalance_ThrowArgumentOutOfRange()
        {
            var options = new DbContextOptionsBuilder<BankAppDataContext>()
                .UseInMemoryDatabase(databaseName: "WithdrawMoreThanBalanceTestDB")
                .Options;

            using (var context = new BankAppDataContext(options))
            {
                // Arrange
                Account temporaryAccount = new Account();
                temporaryAccount.Balance = 500.00m;
                temporaryAccount.AccountId = 34567;
                // Saving the account to DB
                context.Accounts.Add(temporaryAccount);
                context.SaveChanges();
                decimal withdrawalAmount = 600.00m;
                //AccountHandler accountHandler = new AccountHandler(context);
                //accountHandler.Withdrawal(34567, withdrawalAmount);


                // Act
                try
                {
                    accountHandler.Withdrawal(34567, withdrawalAmount);
                }
                catch (ArgumentOutOfRangeException e)
                {
                    // Assert
                    StringAssert.Contains(e.Message, AccountHandler.DebitAmountExceedsBalanceMessage);
                }
            }
        }
        */
    }
}
