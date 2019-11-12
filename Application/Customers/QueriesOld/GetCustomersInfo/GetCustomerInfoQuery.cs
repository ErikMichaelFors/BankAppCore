using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Interfaces;

namespace Application.Customers.Queries.GetCustomersInfo
{
    public class GetCustomerInfoQuery
    {
        private IBankAppDataContext _context { get; set; }

        public GetCustomerInfoQuery(IBankAppDataContext context)
        {
            _context = context;
        }

        public Customer GetCustomer(int customerId)
        {
            return _context.Customers.Where(c => c.CustomerId == customerId).SingleOrDefault();
        }

        public List<Account> GetAccounts(int customerId)
        {
            // TODO
            List<Account> Accounts = new List<Account>();

            //List<Disposition> Dispositions = _context.Dispositions.Where()

            return Accounts;
        }
    }
}
