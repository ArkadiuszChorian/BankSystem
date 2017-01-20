using System.Collections.Generic;
using Service.Models;

namespace Service.Managers
{
    /// <summary>
    /// Class for managing users
    /// </summary>
    public class UserManager
    {
        /// <summary>
        /// Register given user
        /// </summary>
        /// <param name="user"></param>
        public void RegisterUser(User user)
        {
            user.Accounts = new List<string>();
            user.Sessions = new List<string>();
            DAL.Instance.Users.Add(user);           
        }
        /// <summary>
        /// Deletes given user
        /// </summary>
        /// <param name="user"></param>
        public void DeleteUser(User user)
        {
            DAL.Instance.Users.Delete(user);
        }
    }
}
