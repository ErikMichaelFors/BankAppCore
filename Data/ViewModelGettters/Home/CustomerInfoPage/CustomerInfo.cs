using Application.Customers.Queries.GetCustomersInfo;
using System;
using System.Collections.Generic;
using System.Text;
using Application.Models.CustomerModels;
using Application.Interfaces;
using Application.Accounts.Queries.GetAccounts;
using Application.Customers.Queries.GetCustomer;
using System.Linq;
using Domain.Entities;

namespace Persistence.ViewModelGetters.Home.CustomerInfoPage
{
    public class CustomerInfo
    {
        private IBankAppDataContext _context { get; set; }

        public CustomerInfo(IBankAppDataContext context)
        {
            _context = context;
        }

        public CustomerInfoViewModel GetCustomer(int id)
        {
            if (_context.Customers.Where(c => c.CustomerId == id).Count() == 0)
            {
                CustomerInfoViewModel model = new CustomerInfoViewModel();
                model.SetVisible(false);
                return model;
            }
            else
            {
                CustomerInfoViewModel model = new CustomerInfoViewModel();
                model.SetCustomer(new GetCustomer(_context).ByCustomerId(id)); // InfoQuery
                model.SetAccounts(new GetAccounts(_context).OfCustomer(model.Customer));
                return model;
            }
        }

        public CustomerInfoViewModel GetCustomerByAccountId(int accountId)
        {
            return GetCustomer(_context.Dispositions.Where(d => d.AccountId == accountId).First().CustomerId);
        }

        public int GetCustomerIdByAccountId(int accountId)
        {
            return _context.Dispositions.Where(d => d.AccountId == accountId).First().CustomerId;
        }
    }
}
