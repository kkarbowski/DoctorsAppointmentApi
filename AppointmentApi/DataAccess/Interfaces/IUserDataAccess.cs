using AppointmentModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentApi.DataAccess.Interfaces
{
    public interface IUserDataAccess
    {
        public User AddUser(User user);
        public User GetUser(string login);
    }
}
