using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentApi.Tools.Interfaces
{
    public interface IHashGenerator
    {
        public string GenerateHash(string text);
    }
}
