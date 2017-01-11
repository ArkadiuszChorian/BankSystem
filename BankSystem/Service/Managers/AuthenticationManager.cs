using System;
using System.Configuration;
using System.Security.Authentication;
using System.Text;
using Service.Models;
using Service.Providers;

namespace Service.Managers
{
    public class AuthenticationManager
    {
        public SessionIdGenerator SessionIdGenerator { get; set; } = new SessionIdGenerator();

        public string CreateSession(User user)
        {
            var session = new Session(SessionIdGenerator.GenerateId(), user.Id);
            DAL.Instance.Sessions.Add(session);
            user.Sessions.Add(session.Id);
            DAL.Instance.Users.Update(user);

            return session.Id;
        }

        public bool CheckBankCredentials(string encodedCredentials)
        {
            if (string.IsNullOrEmpty(encodedCredentials))
            {
                return false;
            }

            var splittedCredentials = encodedCredentials.Split(' ');

            if (splittedCredentials[0] != "Basic")
            {
                throw new FormatException("Incorrect authentication type. Expected type is Basic. Format of authorization header is Basic Base64(login:password)");
            }

            if (splittedCredentials[1].IsBase64Encoded() == false)
            {
                throw new FormatException("Incorrect Base64 encoding.");
            }
                                 
            var decodedCredentials = Encoding.GetEncoding("ISO-8859-1").GetString(Convert.FromBase64String(splittedCredentials[1]));
            var loginPasswordArray = decodedCredentials.Split(':');

            return loginPasswordArray[0] == ConfigurationManager.AppSettings["BasicAuthLogin"] &&
                   loginPasswordArray[1] == ConfigurationManager.AppSettings["BasicAuthPassword"];
        }

        public string CreateBankCredentials()
        {
            var login = ConfigurationManager.AppSettings["BasicAuthLogin"];
            var password = ConfigurationManager.AppSettings["BasicAuthPassword"];
            var encoded = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(login + ":" + password));

            return "Basic " + encoded;
        }
    }
}

