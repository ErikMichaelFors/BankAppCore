using Application.Interfaces;
using Core.Application.Models.CustomerModels;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Customers.Queries.GetMultipleCustomers
{
    public class GetCustomers
    {
        private IBankAppDataContext _context { get; set; }
        private const int pageLimit = 50;
        private int skippedPages = 0;

        public GetCustomers(IBankAppDataContext context)
        {
            _context = context;
        }

        private void SetNextPageResults(int page)
        {
            skippedPages = page;
        }

        private List<Customer> BasedOnName(string name)
        {
            return _context.Customers.Where(ac => 
            (ac.Givenname + " " + ac.Surname)
            .Contains(name))
            .Skip(pageLimit * skippedPages)
            .Take(pageLimit)
            .ToList();
        }

        private List<Customer> BasedOnCity(string city)
        {
            return _context.Customers.Where(ac =>
            ac.City.Contains(city))
            .Skip(pageLimit * skippedPages)
            .Take(pageLimit)
            .ToList();
        }

        private List<Customer> BasedOnNameAndCity(string name, string city)
        {
            return _context.Customers.Where(ac =>
            (ac.Givenname + " " + ac.Surname)
            .Contains(name) && ac.City.Contains(city))
            .Skip(pageLimit * skippedPages)
            .Take(pageLimit)
            .ToList();
        }

        public SearchCustomerViewModel FilteredByNameAndCity(SearchCustomerViewModel model)//string name, string city, int page)
        {
            // string name = model.Name;
            // string city = model.City;
            int page = model.CurrentPage;

            if (model.Customers != null)
            {
                if (model.Customers.Count() == 50)
                {
                    SetNextPageResults(page++);
                }
                else if (model.Customers.Count() < 50 && page != 0)
                {
                    model.MoreResults = false;
                    SetNextPageResults(page);
                }
            }

            if (model.Name != null && model.City != null)
            {
                model.SetCustomerList(BasedOnNameAndCity(model.Name, model.City));
                return model;
            }
            else if (model.Name != null && model.City == null)
            {
                model.SetCustomerList(BasedOnName(model.Name));
                return model;
            }
            else if (model.Name == null && model.City != null)
            {
                model.SetCustomerList(BasedOnCity(model.City));
                return model;
            }
            model.SetCustomerList(new List<Customer>());
            return model; // Om inget är angivet - ska inte ge något resultat från databasen
        }
    }
}