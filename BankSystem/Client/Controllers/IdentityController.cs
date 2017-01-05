﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BankService;
using Client.ViewModels;
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

        [HttpGet]
        public async Task<RedirectResult> Logout(string returnUrl)
        {
            //var claims = new List<Claim> { new Claim(ClaimTypes.Name, loginViewModel.UserName) };

            //var claimsIdentity = new ClaimsIdentity(claims, "password");
            //var claimsPrinciple = new ClaimsPrincipal(claimsIdentity);

            //var x = await HttpContext.Authentication.GetAuthenticateInfoAsync("Cookies");
            //var z = x.Principal.Identity.Name;

            //await HttpContext.Authentication.SignOutAsync("Cookies");
            await HttpContext.Authentication.SignOutAsync();

            return Redirect("/");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel, string returnUrl)
        {
            var bankService = new BankServiceClient();
            var serviceResponse = await bankService.AuthenticateUserAsync(loginViewModel.UserName, loginViewModel.Password);

            if (serviceResponse == "OK")
            {
                //var claims = new List<Claim>{ new Claim(ClaimTypes.Name, loginViewModel.UserName) };              
                //var claimsIdentity = new ClaimsIdentity(claims);
                //var claimsPrinciple = new ClaimsPrincipal(claimsIdentity);

                //await HttpContext.Authentication.SignInAsync("Cookies", claimsPrinciple);
                var sessionId = await bankService.GenerateSessionIdAsync(loginViewModel.UserName);

                await HttpContext.Authentication.SignInAsync(sessionId);

                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);                  
                }

                return Redirect("/");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Register(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        //public async Task<IActionResult> Register(string userName, string password, string returnUrl)
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel, string returnUrl)
        {
            var bankService = new BankServiceClient();
            var newUser = new User
            {
                UserName = registerViewModel.UserName,
                Password = registerViewModel.Password,
                Sessions = new List<string>(),
                Accounts = new List<string>()
            };

            var serviceResponse = await bankService.RegisterUserAsync(newUser);
            var sessionId = await bankService.GenerateSessionIdAsync(registerViewModel.UserName);

            if (serviceResponse == "OK")
            {
                //var claims = new List<Claim> { new Claim(ClaimTypes.Name, newUser.UserName) };

                //var claimsIdentity = new ClaimsIdentity(claims, "password");
                //var claimsPrinciple = new ClaimsPrincipal(claimsIdentity);

                //await HttpContext.Authentication.SignInAsync("Cookies", claimsPrinciple);

                //await HttpContext.Authentication.SignInAsync(registerViewModel.UserName);
                await HttpContext.Authentication.SignInAsync(sessionId);

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
