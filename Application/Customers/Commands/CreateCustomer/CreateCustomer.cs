using Application.Interfaces;
using Application.Models.CustomerModels;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace Application.Customers.Commands.CreateCustomer
{
    public class CreateCustomer
    {
        //private IBankAppDataContext _context { get; set; }

        //public CreateCustomer(IBankAppDataContext context)
        //{
        //    _context = context;
        //}

        public int Add(IBankAppDataContext _context, CustomerViewModel model)
        {
                Customer customer = new Customer();
            if (model.IsValid())
            {
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

                _context.Customers.Add(customer);
                _context.Save();

                Account account = new Account()
                {
                    Balance = 0,
                    Created = DateTime.Now,
                    Frequency = "Monthly"
                };
                _context.Accounts.Add(account);
                _context.Save();

                Disposition disposition = new Disposition()
                {
                    Account = account,
                    Customer = customer,
                    Type = "Owner"
                };
                _context.Dispositions.Add(disposition);
                _context.Save();
                
            }
            return customer.CustomerId;
        }


        //public async Task<IBankAppDataContext> RunAsync(IBankAppDataContext _context, CustomerViewModel model)
        //public async Task RunAsync(IBankAppDataContext context, CustomerViewModel model)
        //{
        //    var foundCustomer = context.Customers.SingleOrDefault(c => c.CustomerId == model.CustomerId);
        //
        //    if (foundCustomer == null)
        //    {
        //        Customer customer = new Customer();
        //        customer.Birthday = model.Birthday;
        //        customer.City = model.City;
        //        customer.Country = model.Country;
        //        customer.CountryCode = model.CountryCode;
        //        customer.Emailaddress = model.Emailaddress;
        //        customer.Gender = model.Gender;
        //        customer.Givenname = model.Givenname;
        //        customer.Surname = model.Surname;
        //        customer.Telephonecountrycode = model.Telephonecountrycode;
        //        customer.Telephonenumber = model.Telephonenumber;
        //        customer.Zipcode = model.Zipcode;
        //        customer.NationalId = model.NationalId;
        //        customer.Streetaddress = model.Streetaddress;
        //        //context.Customers.Update(customer);
        //        context.Customers.Add(customer);
        //        context.Save();// new System.Threading.CancellationToken());
        //
        //        Account account = new Account()
        //        {
        //            Balance = 0,
        //            Created = DateTime.Now,
        //            Frequency = "Monthly"
        //        };
        //        context.Accounts.Add(account);
        //        context.Save();
        //
        //        Disposition disposition = new Disposition()
        //        {
        //            Account = account,
        //            Customer = customer,
        //            Type = "Owner"
        //        };
        //        context.Dispositions.Add(disposition);
        //    }
        //    else
        //    {
        //        //EditCustomer(foundCustomer, model);
        //    }
        //    context.Save();
        //}
    }
}
