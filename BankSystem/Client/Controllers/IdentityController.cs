using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BankService;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Client.Controllers
{
    public class IdentityController : Controller
    {
        // GET: /<controller>/
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password, string returnUrl)
        {
            var bankService = new BankServiceClient();
            var serviceResponse = await bankService.AuthenticateUserAsync(userName, password);

            if (serviceResponse == "OK")
            {
                var claims = new List<Claim>{ new Claim(ClaimTypes.Name, userName) };

                var claimsIdentity = new ClaimsIdentity(claims, "password");
                var claimsPrinciple = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.Authentication.SignInAsync("Cookies", claimsPrinciple);

                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return Redirect("/");
            }

            return View();
        }
    }
}
