using AppointmentApi.Business.Interfaces;
using AppointmentApi.DataAccess.Interfaces;
using AppointmentApi.Exceptions;
using AppointmentApi.Tools.Interfaces;
using AppointmentModel.Model;
using AppointmentModel.ReturnModel;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentApi.Business
{
    public class UserBusiness : IUserBusiness
    {
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IHashGenerator _hashGenerator;
        private readonly IUserDataAccess _userDataAccess;

        public UserBusiness(IHashGenerator hashGenerator, IUserDataAccess userDataAccess, ITokenGenerator tokenGenerator)
        {
            _tokenGenerator = tokenGenerator;
            _hashGenerator = hashGenerator;
            _userDataAccess = userDataAccess;
        }

        public Token Login(User user)
        {
            try
            {
                var dbUser = _userDataAccess.GetUser(user.Login);
                if (dbUser.Password != _hashGenerator.GenerateHash(user.Password))
                {
                    throw new WrongPasswordException(user.Login);
                }

                return _tokenGenerator.GenerateToken(dbUser);
            } catch (Exception ex)
            {
                Log.Error(ex.Message);
                return null;
            }
        }

        public User Register(User user)
        {
            try
            {
                user.Password = _hashGenerator.GenerateHash(user.Password);
                return _userDataAccess.AddUser(user.NoRoles().NoDateTimeAdd());
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Encountered an exception while executing UserBusiness.Register");
                return null;
            }
        }
    }
}
