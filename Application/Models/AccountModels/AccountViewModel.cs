using Application.Interfaces;
using Application.Transactions.Queries.GetTransactions;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Application.Models.AccountModels
{
    public class AccountViewModel
    {
        public List<Transaction> Transactions { get; set; }
        public int CurrentPage { get; private set; }

        public bool HasMore { get; private set; }

        public void SetPage(int page)
        {
            CurrentPage = page;
        }
    }
}
