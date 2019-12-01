using AppointmentModel.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentModel.ReturnModel
{
    public class Token
    {
        public string TokenString { get; set; }
        public DateTime Expiration { get; set; }
        public int UserId { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
