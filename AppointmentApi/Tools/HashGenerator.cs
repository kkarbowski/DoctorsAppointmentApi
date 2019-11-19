using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentApi.Tools
{
    public static class HashGenerator
    {
        public static string GenerateHash(string text)
        {
            using var sha256 = SHA256.Create();
            var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));
            return BitConverter.ToString(hash);
        }
    }
}
