using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AppointmentModel.Model
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 1)]
        public string Login { get; set; }

        [Required]
        [StringLength(maximumLength: 12, MinimumLength = 6)]
        public string Password { get; set; }

        public DateTime DateTimeAdd { get; set; }

        public List<string> Roles { get; set; }

        // Modifiers

        public User NoRoles()
        {
            Roles = default;
            return this;
        }

        public User NoPassword()
        {
            Password = default;
            return this;
        }

        public User NoUserId()
        {
            UserId = default;
            return this;
        }

        public User NoDateTimeAdd()
        {
            DateTimeAdd = default;
            return this;
        }
    }
}
