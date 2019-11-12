using Application.Interfaces;
using Application.Models.CustomerModels;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Customers.Queries.GetCustomer
{
    public class GetCustomer
    {
        private IBankAppDataContext _context { get; set; }

        public GetCustomer(IBankAppDataContext context)
        {
            _context = context;
        }

        public Customer ByCustomerId(int customerId)
        {
            return _context.Customers.Where(c => c.CustomerId == customerId).SingleOrDefault();
        }

        public Customer CustomerViewModelToCustomer(CustomerViewModel model)
        {
            Customer customer = new Customer();
            customer.CustomerId = model.CustomerId;
            customer.Birthday = model.Birthday;
            customer.City = model.City;
            customer.Country = model.Country;
            customer.CountryCode = model.CountryCode;
            customer.Emailaddress = model.Emailaddress;
            customer.Gender = model.Gender;
            customer.Givenname = model.Givenname;
            customer.Surname = model.Surname;
            customer.Telephonecountrycode = model.Telephonecountrycode;
            customer.Telephonenumber = model.Telephonenumber;
            customer.Zipcode = model.Zipcode;
            customer.NationalId = model.NationalId;
            customer.Streetaddress = model.Streetaddress;
            return customer;
        }
    }
}
