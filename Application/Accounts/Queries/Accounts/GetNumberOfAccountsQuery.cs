using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Accounts.Queries.GetAccountInfo
{
    public class GetNumberOfAccountsQuery
    {
        private IBankAppDataContext _IBankAppDataContext { get; set; }

        public GetNumberOfAccountsQuery(IBankAppDataContext iBankAppDataContext)
        {
            _IBankAppDataContext = iBankAppDataContext;
        }

        public int GetCount()
        {
            return _IBankAppDataContext.Accounts.Count();
        }

        public decimal GetTotalAmountOfAllAccounts()
        {
            return _IBankAppDataContext.Accounts.Sum(a => a.Balance);
        }
        //public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
