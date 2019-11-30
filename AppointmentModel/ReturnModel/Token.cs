using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentModel.ReturnModel
{
    public class Token
    {
        public string TokenString { get; set; }
        public DateTime Expiration { get; set; }
    }
}
