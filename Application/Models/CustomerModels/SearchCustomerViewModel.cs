using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Application.Models.CustomerModels
{
    public class SearchCustomerViewModel
    {
        public string Name { get; set; }
        public string City { get; set; }
        public int CustomerId { get; set; }

        public int CurrentPage { get; set; }

        public bool MoreResults { get; set; }

        public List<Customer> Customers { get; private set; }

        public SearchCustomerViewModel()
        {
            MoreResults = true;
            CurrentPage = 0;
            Customers = new List<Customer>();
        }

        public void SetCustomerList(List<Customer> list)
        {
            Customers = list;
            if (list.Count() < 50)
            {
                MoreResults = false;
            }
            else
            {
                MoreResults = true;
            }
        }
    }
}
