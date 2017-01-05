using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankService;
using Client.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public async Task<IActionResult> Overview()
        {
            var bankService = new BankServiceClient();
            var sessionId = await HttpContext.Authentication.GetSessionId();
            //Todo
            //Get accounts!
            var accounts = await bankService.GetAccountsAsync(sessionId);

            return View(accounts);
        }
        public async Task<IActionResult> History(string id)
        {
            var bankService = new BankServiceClient();
            //var userName = HttpContext.Authentication.GetSessionId();      
            var operations = await bankService.GetAccountHistoryAsync(id);

            return View(operations);
        }

        public IActionResult Transfer(string id)
        {
            ViewData["Message"] = "Your application description page.";
            ViewData["Id"] = id;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Transfer(TransferViewModel transferViewModel, string sourceId, string returnUrl)
        {
            var bankService = new BankServiceClient();

            var operation = new Operation
            {
                SourceId = sourceId,
                DestinationId = transferViewModel.DestinationId,
                Amount = transferViewModel.Amount,
                Title = transferViewModel.Title
            };

            var result = await bankService.TransferAsync(operation);

            return RedirectToAction("Overview");
        }

        public IActionResult Payment()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult WithDraw()
        {
            ViewData["Message"] = "Your contact page.";

            return View("Payment");
        }

        public async Task<IActionResult> CreateAccount()
        {
            var bankService = new BankServiceClient();
            var result = await bankService.CreateAccountAsync(await HttpContext.Authentication.GetSessionId());

            return RedirectToAction("Overview");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}

