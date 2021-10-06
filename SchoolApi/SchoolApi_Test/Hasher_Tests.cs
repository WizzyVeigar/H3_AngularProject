using SchoolApi.Encryption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using Shouldly;
using NUnit.Framework;

namespace SchoolApi_Test
{
    public class Hasher_Tests
    {

        [Test]
        public void HashPassword_HashesTheString()
        {
            string input = "hello";
            string hashed = Hasher.HashPassword(input);

            hashed.ShouldNotBe(input);
        }

       [Test]
        public void HashPassword_HashesWithDifferentSalt()
        {
            string expected = Hasher.HashPassword("hi");
            string actual = Hasher.HashPassword("hi");

            expected.ShouldNotBe(actual);
        }

        [Test]
        public void VerifyPassword_CorrectPassword()
        {
            string attemptedPass = "Hello";
            string correctPass = Hasher.HashPassword("Hello");

            Hasher.VerifyPassword(attemptedPass, correctPass).ShouldBeTrue();
        }

        [Test]
        public void VerifyPassword_IncorrectPassword()
        {
            string attemptedPass = "Hell0";
            string correctPass = Hasher.HashPassword("Hello");

            Hasher.VerifyPassword(attemptedPass, correctPass).ShouldBeFalse();
        }

        [Test]
        public void HashPassword_EmptyStringReturnsNull()
        {
            string hashed = Hasher.HashPassword("");

            hashed.ShouldBe(null);
        }

    }
}
