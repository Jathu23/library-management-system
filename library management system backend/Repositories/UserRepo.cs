using library_management_system.Database.Entiy;

namespace library_management_system.Repositories
{
    public class UserRepo
    {
        ICollection<User> users = new List<User>();



        public User Add(User user)
        {
            users.Add(user);
            return user;
        }
        public User get(string nic)
        {
            var user = users.FirstOrDefault(u => u.NIC == nic);
            return user;
        }
    }
}
