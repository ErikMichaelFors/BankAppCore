using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Customers.Queries.GetCustomersInfo
{
    public class GetNumberOfCustomersQuery //: IBankAppDataContext
    {
        private IBankAppDataContext _IBankAppDataContext { get; set; }

        /*
         DbSet<Account> IBankAppDataContext.Accounts { get; set; }
        DbSet<Card> IBankAppDataContext.Cards { get; set; }
        DbSet<Customer> IBankAppDataContext.Customers { get; set; }
        DbSet<Disposition> IBankAppDataContext.Dispositions { get; set; }
        DbSet<Loan> IBankAppDataContext.Loans { get; set; }
        DbSet<PermenentOrder> IBankAppDataContext.PermenentOrder { get; set; }
        DbSet<Transaction> IBankAppDataContext.Transactions { get; set; }
             */

        public GetNumberOfCustomersQuery(IBankAppDataContext iBankAppDataContext)
        {
            _IBankAppDataContext = iBankAppDataContext;
        }

        public int GetCount()
        {
            return _IBankAppDataContext.Customers.Count();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return _IBankAppDataContext.SaveChangesAsync(cancellationToken);
        }
    }
}
