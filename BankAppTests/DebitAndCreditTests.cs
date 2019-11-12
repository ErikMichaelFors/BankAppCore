using Application.Accounts.Commands.EditAccount;
using Application.Transactions.Commands.CreateTransaction;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Linq;
using Xunit;

namespace BankAppTests
{
    public class DebitAndCreditTests : DbContext
    {

        private DbContextOptions<BankAppDataContext> _options;

        public DebitAndCreditTests()
        {
            _options = new DbContextOptionsBuilder<BankAppDataContext>()
                .UseInMemoryDatabase(databaseName: "Commands_To_DB")
                .Options;
        }

        [Fact] // TODO
        public void Withdrawal_CantWithdrawMoreThanBalance()
        {
            using (var context = new BankAppDataContext(_options))
            {
                int accountId = 12345;
                // Arrange
                Account temporaryAccount = new Account();
                temporaryAccount.Balance = 500.00m;
                temporaryAccount.AccountId = accountId;
                decimal expectedBalance = temporaryAccount.Balance;

                // Saving the account to DB
                context.Accounts.Add(temporaryAccount);
                context.SaveChanges();

                decimal withdrawalAmount = 600.00m;

                // Act
                Action act = () =>
                {
                    new AccountHandler().Withdrawal(accountId, withdrawalAmount, context);
                };

                // Assert
                var account = context.Accounts.Where(a => a.AccountId == accountId).SingleOrDefault();
                Assert.Equal(expectedBalance, account.Balance);
            }
        }

        [Fact] // TODO
        public void Withdrawal_CantWithdrawNegativeAmounts()
        {
            using (var context = new BankAppDataContext(_options))
            {
                int accountId = 23456;
                // Arrange
                Account temporaryAccount = new Account();
                temporaryAccount.Balance = 500.00m;
                temporaryAccount.AccountId = accountId;
                decimal expectedBalance = temporaryAccount.Balance;

                // Saving the account to DB
                context.Accounts.Add(temporaryAccount);
                context.SaveChanges();

                decimal withdrawalAmount = -300.00m;

                // Act
                Action act = () =>
                {
                    new AccountHandler().Withdrawal(accountId, withdrawalAmount, context);
                };

                // Assert
                var account = context.Accounts.Where(a => a.AccountId == accountId).SingleOrDefault();
                Assert.Equal(expectedBalance, account.Balance);
            }
        }

        [Fact]
        public void Transfer_CantTransferMoreThanBalance()
        {
            using (var context = new BankAppDataContext(_options))
            {
                // Arrange
                int accountIdFrom = 34567;
                int accountIdTo = 45678;

                Account temporaryFromAccount = new Account();
                temporaryFromAccount.Balance = 500.00m;
                temporaryFromAccount.AccountId = accountIdFrom;
                decimal expectedFrom = temporaryFromAccount.Balance;

                Account temporaryToAccount = new Account();
                temporaryToAccount.Balance = 300.00m;
                temporaryToAccount.AccountId = accountIdTo;
                decimal expectedTo = temporaryToAccount.Balance;
                // Saving the account to DB
                context.Accounts.Add(temporaryFromAccount);
                context.Accounts.Add(temporaryToAccount);
                context.SaveChanges();
                decimal transferAmount = 800.00m;

                // Act
                Action act = () =>
                {
                    new CreateTransaction(context).Transfer(accountIdFrom, accountIdTo, transferAmount);
                };

                // Assert
                var fromAcc = context.Accounts.Where(a => a.AccountId == accountIdFrom).SingleOrDefault();
                var toAcc = context.Accounts.Where(a => a.AccountId == accountIdTo).SingleOrDefault();
                // Assert
                Assert.Equal(expectedFrom, fromAcc.Balance);
                Assert.Equal(expectedTo, toAcc.Balance);
            }
        }

        [Fact]
        public void Transfer_CantTransferNegativeAmounts()
        {
            using (var context = new BankAppDataContext(_options))
            {
                // Arrange
                int accountIdFrom = 56789;
                int accountIdTo = 67890;

                Account temporaryFromAccount = new Account();
                temporaryFromAccount.Balance = 500.00m;
                temporaryFromAccount.AccountId = accountIdFrom;
                decimal expectedFrom = temporaryFromAccount.Balance;

                Account temporaryToAccount = new Account();
                temporaryToAccount.Balance = 300.00m;
                temporaryToAccount.AccountId = accountIdTo;
                decimal expectedTo = temporaryToAccount.Balance;
                // Saving the account to DB
                context.Accounts.Add(temporaryFromAccount);
                context.Accounts.Add(temporaryToAccount);
                context.SaveChanges();
                decimal transferAmount = -800.00m;
                // Act
                Action act = () =>
                {
                    new CreateTransaction(context).Transfer(accountIdFrom, accountIdTo, transferAmount);
                };
                var fromAcc = context.Accounts.Where(a => a.AccountId == accountIdFrom).SingleOrDefault();
                var toAcc = context.Accounts.Where(a => a.AccountId == accountIdTo).SingleOrDefault();
                // Assert
                Assert.Equal(expectedFrom, fromAcc.Balance);
                Assert.Equal(expectedTo, toAcc.Balance);
            }
        }

        [Fact]
        public void Deposit_CantDepositwNegativeAmounts()
        {
            using (var context = new BankAppDataContext(_options))
            {
                // Arrange
                int accountId = 78901;

                Account temporaryAccount = new Account();
                temporaryAccount.Balance = 500.00m;
                temporaryAccount.AccountId = accountId;
                decimal expected = temporaryAccount.Balance;

                // Saving the account to DB
                context.Accounts.Add(temporaryAccount);
                context.SaveChanges();
                decimal amount = -800.00m;
                // Act & Assert
                Action act = () =>
                {
                    bool actual = new AccountHandler().Deposit(accountId, amount, context);
                    Assert.False(actual);
                };
                var account = context.Accounts.Where(a => a.AccountId == temporaryAccount.AccountId).SingleOrDefault();
                // Assert
                Assert.Equal(expected, account.Balance);
                
            }
        }
    }
}
