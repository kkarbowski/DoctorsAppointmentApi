using AppointmentModel.Model;
using AppointmentModel.ReturnModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentApi.Business.Interfaces
{
    public interface IUserBusiness
    {
        public Token Login(User user);

        public User Register(User user);
    }
}
