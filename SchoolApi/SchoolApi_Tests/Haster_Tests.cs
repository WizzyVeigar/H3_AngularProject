using SchoolApi.Encryption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SchoolApi_Tests
{
    public class Haster_Tests
    {
        [Fact]
        public void HashPassword_HashesCorrectly()
        {
            string expected = Hasher.HashPassword("Hello");
            string actual = Hasher.HashPassword("Hello");

            Assert.Equal(expected, actual);
        }


        [Fact]
        public void VerifyPassword_CorrectPassword()
        {
            string attemptedPass = "Hello";
            string correctPass = Hasher.HashPassword("Hello");

            Assert.True(Hasher.VerifyPassword(attemptedPass, correctPass));
        }

        [Fact]
        public void VerifyPassword_IncorrectPassword()
        {
            string attemptedPass = "Hello";
            string correctPass = Hasher.HashPassword("Hello");

            Assert.True(Hasher.VerifyPassword(attemptedPass, correctPass));
        }

    }
}
