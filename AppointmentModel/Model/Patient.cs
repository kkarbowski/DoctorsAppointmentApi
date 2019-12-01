using AppointmentModel.Model;
using System;
using System.ComponentModel.DataAnnotations;

namespace AppointmentModel
{
    [Serializable]
    public class Patient : Human
    {
        public string Phone { get; set; }

        public string Mail { get; set; }

        public DateTime BirthDate { get; set; }
    }
}
