using Application.Accounts.Queries.GetAccountInfo;
using Application.Customers.Queries.GetCustomersInfo;
using Application.Interfaces;
using Core.Application.Models.HomeModels;
using Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.ViewModelLoaders.Home.StartPage
{
    public class HomeIndex
    {
        private IBankAppDataContext _context { get; set; }//= new BankAppDataContext();

        public HomeIndex(IBankAppDataContext context)
        {
            _context = context;
        }

        public StartPageViewModel GetModel()//IBankAppDataContext _context)
        {
            StartPageViewModel model = new StartPageViewModel();
            model.SetNrCustomers(new GetNumberOfCustomersQuery(_context).GetCount());
            model.SetNrAccounts(new GetNumberOfAccountsQuery(_context).GetCount());
            model.SetSumAllAccounts(new GetNumberOfAccountsQuery(_context).GetTotalAmountOfAllAccounts());
            return model;
        }
    }
}
