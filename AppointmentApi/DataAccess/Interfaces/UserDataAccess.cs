using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentApi.Database;
using AppointmentModel.Model;

namespace AppointmentApi.DataAccess.Interfaces
{
    public class UserDataAccess : IUserDataAccess
    {
        private readonly AppDbContext _appDbContext;

        public UserDataAccess(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public User AddUser(User user)
        {
            var newUser = _appDbContext.Users.Add(user);
            _appDbContext.SaveChanges();

            return newUser.Entity;
        }

        public User GetUser(string login)
        {
            return _appDbContext.Users.Single(u => u.Login == login);
        }
    }
}
