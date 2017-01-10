using System.Linq;
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
    }
}
