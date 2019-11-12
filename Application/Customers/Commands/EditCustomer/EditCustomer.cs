using Application.Interfaces;
using Application.Models.CustomerModels;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Customers.Commands.EditCustomer
{
    public class EditCustomer
    {
        private IBankAppDataContext _context { get; set; }

        public EditCustomer(IBankAppDataContext context)
        {
            _context = context;
        }

        public CustomerViewModel Edit(CustomerViewModel model)
        {
            if (model.IsValid() && model.CustomerId != 0)
            {
                Customer customer = _context.Customers.Where(c => c.CustomerId == model.CustomerId).SingleOrDefault();
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
                _context.Customers.Update(customer);
                _context.Save();//new System.Threading.CancellationToken()); // Hope this works
                return model;
            }
            return model;
        }
    }
}
