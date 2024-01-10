using DAL.Entities;
using DAL.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserRepositoryDb : IUserRepository
    {

        private readonly string _connectionString = "Server=localhost;Database=DevNetCloudDB;Trusted_Connection=True;TrustServerCertificate=True";

        public User Create(User user)
        {
            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                using(SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Users OUTPUT inserted.Id VALUES(@email, @password, @pseudo)";

                    cmd.Parameters.AddWithValue("email", user.Email);
                    cmd.Parameters.AddWithValue("password", user.Password);
                    cmd.Parameters.AddWithValue("pseudo", user.Pseudo);

                    conn.Open();

                    user.Id = Convert.ToInt32(cmd.ExecuteScalar());

                    conn.Close();

                    return user;

                }
            }
        }

        public bool Delete(User user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User? GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public User? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
