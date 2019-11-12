using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Application.Accounts.Queries.GetAccountsInfo;
using Application.Accounts.Queries.GetAccountInfo;
using Application.Interfaces;
using Persistence.ViewModelLoaders.Home.StartPage;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private IBankAppDataContext _context;
        public HomeController(IBankAppDataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(new HomeIndex(_context).GetModel());
        }
    }
}
