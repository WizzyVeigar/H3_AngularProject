using SchoolApi.Encryption;
using SchoolApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolApi.Managers
{
    public class UserManager : IDisposable
    {
        private SchoolContext context;
        public UserManager(SchoolContext con)
        {
            context = con;
        }

        public User GetDatabaseUserFromUsername(string username)
        {
            return context.User.Where(user => user.Username == username).FirstOrDefault();
        }

        public User GetDatabaseUserFromTokenString(string tokenString)
        {
            var data = (from user in context.User
                        join token in context.IssuedToken
                        on user.Username equals token.Username
                        where token.TokenString == tokenString
                        select new User { Username = user.Username, Password = user.Password }).SingleOrDefault();

            User u = (User)data;
            return u;
        }

        public bool VerifyLogin(string attemptPassword, string correctPass)
        {
            return Hasher.VerifyPassword(attemptPassword, correctPass);
        }

        public bool CreateUser(string username, string password)
        {
            if (username.Trim() == string.Empty || password.Trim() == string.Empty)
                return false;
            //Checks if username already exists
            if (GetDatabaseUserFromUsername(username) == null)
            {
                string hashedPass = Hasher.HashPassword(password);
                context.User.Add(new User { Username = username, Password = hashedPass });
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            context.Dispose();
        }
    }
}
