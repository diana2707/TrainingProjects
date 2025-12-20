using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AirportTool.Application.Utils
{
    public static class ConfirmationCodeGenerator
    {
        public static string Generate(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            using var rng = RandomNumberGenerator.Create();
            var bytes = new byte[length];
            rng.GetBytes(bytes);

            return new string(bytes.Select(b => chars[b % chars.Length]).ToArray());
        }
    }
}
