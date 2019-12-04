using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentApi.Exceptions
{
    public class WrongPasswordException : Exception
    {
        public WrongPasswordException(string login)
            : base($"Wrong password for user {login}") 
        { 
        }
    }
}
