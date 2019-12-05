using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentModel.Model
{
    public static class Role
    {
        public const string Admin = "Admin";
        public const string Patient = "Patient";
        public const string Doctor = "Doctor";

        public const string Doctor_Patient = Doctor + "," + Patient;
    }
}
