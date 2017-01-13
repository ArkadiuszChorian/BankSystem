using System.Collections.Generic;
using Service.Models;

namespace Service.Managers
{
    public class UserManager
    {
        public void RegisterUser(User user)
        {
            user.Accounts = new List<string>();
            user.Sessions = new List<string>();
            DAL.Instance.Users.Add(user);           
        }

        public void DeleteUser(User user)
        {
            DAL.Instance.Users.Delete(user);
        }
    }
}
