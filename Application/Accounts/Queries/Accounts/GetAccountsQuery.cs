using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Accounts.Queries.GetAccountsInfo
{
    public class GetAccountsQuery
    {
        private IBankAppDataContext _context { get; set; }

        public GetAccountsQuery(IBankAppDataContext context)
        {
            _context = context;
        }

        public List<Account> GetAccouontByCustomerId(int customerId)
        {
            //for
            return new List<Account>();
        }
    }
}
