using System;
using System.Configuration;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using Service.Generators;
using Service.Models;

namespace Service.Managers
{
    /// <summary>
    /// Class for managing authentication
    /// </summary>
    public class AuthenticationManager
    {
        private SessionIdGenerator _sessionIdGenerator = new SessionIdGenerator();

        /// <summary>
        /// Creates session from given user name
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>String representing id of created session</returns>
        public string CreateSession(string userName)
        {
            var user = DAL.Instance.Users.First(user2 => user2.UserName == userName);
            var session = new Session(_sessionIdGenerator.GenerateId(), user.Id);
            DAL.Instance.Sessions.Add(session);
            user.Sessions.Add(session.Id);
            DAL.Instance.Users.Update(user);

            return session.SessionId;
        }
        /// <summary>
        /// Checks if bank with given credentials is authenticated
        /// </summary>
        /// <param name="encodedCredentials"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Creates new bank credentials
        /// </summary>
        /// <returns></returns>
        public string CreateBankCredentials()
        {
            var login = ConfigurationManager.AppSettings["BasicAuthLogin"];
            //var password = ConfigurationManager.AppSettings["BasicAuthPassword"];
            var password = "ninja";
            var encoded = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(login + ":" + password));

            return "Basic " + encoded;
        }
        /// <summary>
        /// Checks user credentials
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool ChceckUserCredentials(string userName, string password)
        {
            try
            {
                var user = DAL.Instance.Users.Single(user2 => user2.UserName == userName);

                if (user.Password != password)
                {
                    throw new AuthenticationException("Username, password or both are incorrect.");
                }
            }
            catch (InvalidOperationException exception)
            {
                throw new AuthenticationException("Username, password or both are incorrect.");
            }

            return true;
        }
        /// <summary>
        /// Gets user from given session id
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public User GetUserFromSessionId(string sessionId)
        {
            var session = DAL.Instance.Sessions.Single(session2 => session2.SessionId == sessionId);
            var user = DAL.Instance.Users.Single(user2 => user2.Id == session.UserId);

            return user;
        }
    }
}

