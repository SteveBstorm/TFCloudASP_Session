using BLL.Forms;
using BLL.Mapper;
using DAL.Entities;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserService
    {

        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User? Create(RegisterForm form)
        {
            User? u = _userRepository.GetByEmail(form.Email);

            if (u != null)
            {
                User user = _userRepository.Create(form.ToUser());
                return user;
            }

            return null;
            
        }

        public bool EmailAlreadyUsed(string email)
        {
            return _userRepository.GetByEmail(email) != null;
        }

    }
}
