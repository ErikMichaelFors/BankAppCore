using Application.Interfaces;
using Application.Models.AccountModels;
using Application.Transactions.Queries.GetTransactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebUI.Controllers
{
    [Authorize(Roles = "cashier")]
    public class AccountController : Controller
    {
        private IBankAppDataContext _context { get; set; }
        public AccountController(IBankAppDataContext context)
        {
            _context = context;
        }

        
        public IActionResult DisplayTransactions(int accountId)
        {
            return View(new GetTransactions(_context).ByAccountId(accountId, 0));
        }

        [HttpPost]
        public IActionResult DisplayTransactions(int accountId, int page = 0)
        {
            if (page > 0)
            {
                return PartialView("_ListNextTransactions", new GetTransactions(_context).ByAccountId(accountId, page));
            }
            return View(new GetTransactions(_context).ByAccountId(accountId, page));
        }

        /*  DEPOSIT  */
        // Skapar upp Vy-modellen och redirectar
        public IActionResult MakeDeposit(int accountId)
        {
            DepositViewModel model = new DepositViewModel();
            model.Account = accountId;
            return View(model);
        }
        // Skickar till Deposit-vyn
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MakeDeposit(DepositViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.MakeDeposit(_context))
                    return RedirectToAction("DepositSuccessful", model);
            }
            return View(model);
        }
        // Om deposit lyckats
        public IActionResult DepositSuccessful(DepositViewModel model)
        {
            return View(model);
        }

        /*  WITHDRAWAL  */
        // Skapar upp Vy-modellen och redirectar
        public IActionResult MakeWithdrawal(int accountId)
        {
            WithdrawalViewModel model = new WithdrawalViewModel();
            model.Account = accountId;
            return View(model);
        }
        // Skickar till Withdrawal-vyn
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MakeWithdrawal(WithdrawalViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.MakeWithdrawal(_context))
                    return RedirectToAction("WithdrawalSuccessful", model);
            }
            return View(model);
        }
        // Om Withdrawal lyckats
        public IActionResult WithdrawalSuccessful(WithdrawalViewModel model)
        {
            return View(model);
        }
        /*  TRANSFER  */
        // Skapar upp Vy-modellen
        public IActionResult MakeTransfer(int accountId)
        {
            TransferViewModel model = new TransferViewModel();
            model.FromAccount = accountId;
            return View(model);
        }
        // Skickar till Withdrawal-vyn
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MakeTransfer(TransferViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.MakeTransfer(_context))
                    return RedirectToAction("TransferSuccessful", model);
            }
            return View(model);
        }
        // Om Withdrawal lyckats
        public IActionResult TransferSuccessful(TransferViewModel model)
        {
            return View(model);
        }

        public IActionResult MakeReadyWithdrawal(int accountId)
        {
            AccountTransactionViewModel model = new AccountTransactionViewModel(_context);
            model.SetFromAccount(accountId);
            return View();
        }

        public IActionResult MakeReadyTransfer(int accountId)
        {
            AccountTransactionViewModel model = new AccountTransactionViewModel(_context);
            model.SetFromAccount(accountId);
            return View(model);
        }
    }
}