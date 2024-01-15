using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUserRepository
    {

        public void Create(User user);

        public User? GetByEmail(string email);

        public User? GetById(int id);

        public IEnumerable<User> GetAll(string token);

        public bool Update(UpdatePasswordDAL user);

        public bool Delete(User user);
        public string Login(string email, string password);

    }
}
