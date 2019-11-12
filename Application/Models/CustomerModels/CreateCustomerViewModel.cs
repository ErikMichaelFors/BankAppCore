using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models.CustomerModels
{
    public class CreateCustomerViewModel
    {
        public Customer Customer { get; set; }

        public CreateCustomerViewModel()
        {

        }

        public CreateCustomerViewModel(string gender, string givenname, string surname, string streetadress, string city, string zipcode, string country, string countrycode)
        { // Only the mandatory information
            Customer.Gender = gender;
            Customer.Givenname = givenname;
            Customer.Surname = surname;
            Customer.Streetaddress = streetadress;
            Customer.City = city;
            Customer.Zipcode = zipcode;
            Customer.Country = country;
            Customer.CountryCode = countrycode;
        }

        public void AddNonMandatoryInfo(DateTime? birthday, string nationalid, string telephonecountrycode, string telephonenumber, string emailadress)
        {
            Customer.Birthday = birthday;
            Customer.NationalId = nationalid;
            Customer.Telephonecountrycode = telephonecountrycode;
            Customer.Telephonenumber = telephonenumber;
            Customer.Emailaddress = emailadress;
        }
    }
}
