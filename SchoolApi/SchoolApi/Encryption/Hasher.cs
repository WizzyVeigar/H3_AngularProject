using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Crypt = BCrypt.Net.BCrypt;

namespace SchoolApi.Encryption
{
    public class Hasher
    {
        /// <summary>
        /// Hashes a password with BCrypt
        /// </summary>
        /// <param name="password"></param>
        /// <returns>Returns the hashed passwrod</returns>
        public static string HashPassword(string password)
        {
            if (password.Length > 0)
                return Crypt.EnhancedHashPassword(password, 12);
            return null;

        }

        /// <summary>
        /// verifies an attempted password against the stored one
        /// </summary>
        /// <param name="attemptedPass">The password from login page</param>
        /// <param name="correctPass">The password from database</param>
        /// <returns>Returns true if password is correct</returns>
        public static bool VerifyPassword(string attemptedPass, string correctPass)
        {
            return Crypt.Verify(attemptedPass, correctPass, true);
        }
    }
}
