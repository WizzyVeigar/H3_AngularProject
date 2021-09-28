using System;
using SchoolApi.Encryption;

namespace testing
{
    class Program
    {
        static void Main(string[] args)
        {
            string hashPass = Hasher.HashPassword("HelloWorld");

            bool loginSuccess = Hasher.VerifyPassword("HellOWorld", hashPass);

            Console.WriteLine(hashPass + "\n" + loginSuccess);
            Console.ReadLine();
        }
    }
}
