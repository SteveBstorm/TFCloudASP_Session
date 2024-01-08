using DAL.Context;
using DAL.Entities;

namespace DAL.Repositories
{
    public class UserRepository
    {

        public User Create(User user)
        {
            int Id = FakeDB.Users.Max(x => x.Id) + 1;

            user.Id = Id;

            FakeDB.Users.Add(user);

            return user;
        }

        public User? GetByEmail(string email)
        {
            User? u = FakeDB.Users.SingleOrDefault(x => x.Email == email);

            return u;
        }
    }
}
