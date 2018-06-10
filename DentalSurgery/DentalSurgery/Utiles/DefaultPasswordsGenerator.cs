using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DentalSurgery.Utiles
{
    public class DefaultPasswordsGenerator
    {
        private static Random random = new Random();
        public static string GenerateDefaultPassword(int length)
        {
            const string chars = "abcdefghijklmnoprstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}