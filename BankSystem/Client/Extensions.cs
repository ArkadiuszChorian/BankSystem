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
        public static Task SignInAsync(this AuthenticationManager authenticationManager, string sessionId)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Sid, sessionId) };
            var claimsIdentity = new ClaimsIdentity(claims, AuthenticationTypes.Password);
            var claimsPrinciple = new ClaimsPrincipal(claimsIdentity);

            return authenticationManager.SignInAsync(AuthenticationMiddlewareName, claimsPrinciple);
        }

        public static Task SignOutAsync(this AuthenticationManager authenticationManager)
        {
            return authenticationManager.SignOutAsync(AuthenticationMiddlewareName);
        }

        public static async Task<string> GetSessionId(this AuthenticationManager authenticationManager)
        {
            var authenticateInfo = await authenticationManager.GetAuthenticateInfoAsync(AuthenticationMiddlewareName);

            return authenticateInfo.Principal.FindFirst(claim => claim.Type == ClaimTypes.Sid).Value;
        }
    }
}
