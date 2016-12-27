using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Authentication;

namespace Client
{
    public static class Extensions
    {
        private const string AuthenticationMiddlewareName = "Cookies";
        public static Task SignInAsync(this AuthenticationManager authenticationManager, string userName)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, userName) };
            var claimsIdentity = new ClaimsIdentity(claims);
            var claimsPrinciple = new ClaimsPrincipal(claimsIdentity);

            return authenticationManager.SignInAsync(AuthenticationMiddlewareName, claimsPrinciple);
        }

        public static Task SignOutAsync(this AuthenticationManager authenticationManager)
        {
            return authenticationManager.SignInAsync(AuthenticationMiddlewareName);
        }

        public static async Task<string> GetUserName(this AuthenticationManager authenticationManager)
        {
            var authenticateInfo = await authenticationManager.GetAuthenticateInfoAsync(AuthenticationMiddlewareName);

            return authenticateInfo.Principal.Identity.Name;
        }
    }
}
