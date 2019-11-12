using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models.CustomerModels
{
    public class CustomerInfoViewModel
    {
        public Customer Customer { get; set; }
        public List<Account> Accounts { get; set; }

        public string ErrorMessage { get; private set; }

        public bool DisplayCustomer { get; private set; }

        public decimal SumTotal { get; private set; }

        public void SetVisible(bool visible)
        {
            DisplayCustomer = visible;
            if (DisplayCustomer == false)
            {
                ErrorMessage = "There is no customer with that customer ID";
            }
        }

        public void SetCustomer(Customer customer)
        {
            SetVisible(true);
            Customer = customer;
        }

        public void SetAccounts(List<Account> accounts)
        {
            Accounts = accounts;
            foreach (var account in Accounts)
            {
                SumTotal += account.Balance;
            }
        }
    }
}
