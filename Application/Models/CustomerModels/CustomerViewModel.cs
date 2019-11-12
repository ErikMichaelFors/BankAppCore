using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Models.CustomerModels
{
    public class CustomerViewModel
    {
        public int CustomerId { get; set; }
        [Required]
        [StringLength(6)]
        public string Gender { get; set; }
        [Required]
        [StringLength(100)]
        public string Givenname { get; set; }
        [Required]
        [StringLength(100)]
        public string Surname { get; set; }
        [Required]
        [StringLength(100)]
        public string Streetaddress { get; set; }
        [Required]
        [StringLength(100)]
        public string City { get; set; }
        [Required]
        [StringLength(15)]
        public string Zipcode { get; set; }
        [Required]
        [StringLength(100)]
        public string Country { get; set; }
        [Required]
        [StringLength(2)]
        public string CountryCode { get; set; }

        public DateTime? Birthday { get; set; }
        [StringLength(20)]
        public string NationalId { get; set; }
        [StringLength(10)]
        public string Telephonecountrycode { get; set; }
        [StringLength(25)]
        public string Telephonenumber { get; set; }
        [StringLength(100)]
        public string Emailaddress { get; set; }

        public CustomerViewModel()
        {

        }

        public CustomerViewModel(Customer customer)
        {
            CustomerId = customer.CustomerId;
            Gender = customer.Gender;
            Givenname = customer.Givenname;
            Surname = customer.Surname;
            Telephonecountrycode = customer.Telephonecountrycode;
            Telephonenumber = customer.Telephonenumber;
            Zipcode = customer.Zipcode;
            NationalId = customer.NationalId;
            Streetaddress = customer.Streetaddress;

            if (customer.Birthday != null)
                Birthday = customer.Birthday;
            if (customer.City != null)
                City = customer.City;
            if (customer.Country != null)
                Country = customer.Country;
            if (customer.CountryCode != null)
                CountryCode = customer.CountryCode;
            if (customer.Emailaddress != null)
                Emailaddress = customer.Emailaddress;
        }

        public bool IsValid()
        {
            if (Gender != null &&
               Givenname != null &&
               Surname != null &&
               Streetaddress != null &&
               City != null &&
               Zipcode != null &&
               Country != null &&
               CountryCode != null)
            {
                return true;
            }
            return false;
        }
    }
}
