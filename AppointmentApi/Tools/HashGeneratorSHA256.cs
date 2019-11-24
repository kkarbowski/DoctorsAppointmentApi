using AppointmentApi.Tools.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentApi.Tools
{
    public class HashGeneratorSHA256 : IHashGenerator
    {
        public string GenerateHash(string text)
        {
            using var sha256 = SHA256.Create();
            var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));
            return BitConverter.ToString(hash);
        }
    }
}
