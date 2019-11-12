using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Accounts.Queries.GetAccounts
{
    public class GetAccounts
    {
        private IBankAppDataContext _context { get; set; }

        public GetAccounts(IBankAppDataContext context)
        {
            _context = context;
        }

        public List<Account> OfCustomer(Customer customer)
        {
            if (customer == null)
            {
                return null;
            }
            var test = _context.Customers.Where(c => c.CustomerId == customer.CustomerId).SingleOrDefault();
            if (test == null)
            {
                return null;
            }
            else
            {
                List<Disposition> Dispositions = _context.Dispositions.Where(d => d.CustomerId == customer.CustomerId).ToList();
                List<int> AccountIds = Dispositions.Select(d => d.AccountId).Distinct().ToList();

                List<Account> Accounts = new List<Account>();
                foreach (var accountId in AccountIds)
                {
                    Accounts.AddRange(_context.Accounts.Where(a => a.AccountId == accountId).ToList());
                }
                Accounts.Distinct().ToList();
                return Accounts;
            }
        }
    }
}
