using Service.Models;

namespace Service.Managers
{
    public class UserManager
    {
        public void RegisterUser(User user)
        {
            DAL.Instance.Users.Add(user);           
        }

        public void DeleteUser(User user)
        {
            DAL.Instance.Users.Delete(user);
        }
    }
}
