using SchoolApi.Encryption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using NUnit.Framework;

namespace SchoolApi_Test
{
    public class Hasher_Tests
    {
        [Test]
        public void HashPassword_HashesCorrectly()
        {
            string expected = Hasher.HashPassword("Hello");
            string actual = Hasher.HashPassword("Hello");

            Assert.AreEqual(expected, actual);
            //Assert.Equal(expected, actual);
        }


        public void VerifyPassword_CorrectPassword()
        {
            string attemptedPass = "Hello";
            string correctPass = Hasher.HashPassword("Hello");

            //Assert.True(Hasher.VerifyPassword(attemptedPass, correctPass));
        }

        public void VerifyPassword_IncorrectPassword()
        {
            string attemptedPass = "Hello";
            string correctPass = Hasher.HashPassword("Hello");

            //Assert.True(Hasher.VerifyPassword(attemptedPass, correctPass));
        }

    }
}
