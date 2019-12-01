using AppointmentModel.Model;
using System;
using System.ComponentModel.DataAnnotations;

namespace AppointmentModel
{
    [Serializable]
    public class Patient : Human
    {
        [DisplayFormat(NullDisplayText = "-")]
        public string Phone { get; set; }

        [DisplayFormat(NullDisplayText = "-")]
        public string Mail { get; set; }

        [DisplayFormat(NullDisplayText = "-")]
        public DateTime BirthDate { get; set; }
    }
}
