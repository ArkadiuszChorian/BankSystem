using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankService;
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
        public async Task<IActionResult> History(string accountId)
        {
            var bankService = new BankServiceClient();
            //var userName = HttpContext.Authentication.GetSessionId();      
            var operations = await bankService.GetAccountHistoryAsync(accountId);

            return View(operations);
        }

        public IActionResult Transfer()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Payment()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
