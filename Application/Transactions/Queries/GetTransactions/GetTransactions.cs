using Application.Interfaces;
using Core.Application.Models.AccountModels;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Transactions.Queries.GetTransactions
{
    public class GetTransactions
    {
        private IBankAppDataContext _context { get; set; }
        private const int numberOfTransactionsToLoad = 20;

        public GetTransactions(IBankAppDataContext context)
        {
            _context = context;
        }

        public AccountViewModel ByAccountId(int accountId, int blockOfTwenty)
        {
            AccountViewModel model = new AccountViewModel();

            model.Transactions = _context.Transactions
                .OrderByDescending(t => t.TransactionId)
                .Where(t => t.AccountId == accountId)
                .Skip(blockOfTwenty * numberOfTransactionsToLoad)
                .Take(numberOfTransactionsToLoad).ToList();
            model.SetPage(blockOfTwenty);

            return model;
        }
    }
}
