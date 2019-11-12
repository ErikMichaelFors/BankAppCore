using Application.Customers.Commands.CreateCustomer;
using Application.Customers.Commands.EditCustomer;
using Application.Customers.Queries.GetCustomer;
using Application.Customers.Queries.GetMultipleCustomers;
using Application.Interfaces;
using Application.Models.CustomerModels;
using Core.Application.Models.CustomerModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.ViewModelGetters.Home.CustomerInfoPage;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebUI.Controllers
{
    [Authorize(Roles = "cashier")]
    public class CustomerController : Controller
    {
        private IBankAppDataContext _context;
        public CustomerController(IBankAppDataContext context)
        {
            _context = context;
        }

        
        public IActionResult CustomerInfo(int CustomerId)
        {
            return View(new CustomerInfo(_context).GetCustomer(CustomerId));
        }

        public IActionResult GoToCustomerInfo(int AccountId)
        {
            int id = new CustomerInfo(_context).GetCustomerIdByAccountId(AccountId);
            return RedirectToAction("CustomerInfo", new { CustomerId = id });
        }

        public IActionResult SearchCustomer()
        {
            return View(new SearchCustomerViewModel());
        }

        [HttpPost]
        public IActionResult DisplayCustomerSearchList(SearchCustomerViewModel model, int page)
        {
            model.CurrentPage = page;
            model = new GetCustomers(_context).FilteredByNameAndCity(model);
            return View(model);
        }

        public IActionResult CreateCustomer()
        {
            return View(new CustomerViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCustomer(CustomerViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("CustomerInfo", new { CustomerId = new CreateCustomer().Add(_context, model) });
            }
            return View(model);
        }

        public IActionResult DisplaySavedCustomer(CustomerViewModel model)
        {
            if (model.CustomerId != 0)
            {
                return RedirectToAction("CustomerInfo", new CreateCustomer().Add(_context, model));
            }
            return RedirectToAction("CreateCustomer", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCustomer(CustomerViewModel model)
        {
            return View(new EditCustomer(_context).Edit(model));
        }

        public IActionResult EditCustomer(int id)
        {
            return View(new CustomerViewModel(new GetCustomer(_context).ByCustomerId(id)));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveEditedCustomer(CustomerViewModel vm)
        {
            if (vm != null) EditCustomer(vm);
            return RedirectToAction("EditCustomer", new { id = vm.CustomerId });
        }
    }
}
