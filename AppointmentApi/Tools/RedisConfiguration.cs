using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentApi.Tools
{
    public static class RedisConfiguration
    {
        public static string GetRedisIp()
        {
            string ip = Environment.GetEnvironmentVariable("REDIS_IP");
            if (ip == null)
            {
                return BASE_IP;
            }
            return ip;
        }

        private const string BASE_IP = "127.0.0.1";
    }
}
