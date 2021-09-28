﻿using SchoolApi.Encryption;
using SchoolApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolApi
{
    public class UserManager
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

        public bool VerifyLogin(string attemptPassword, string correctPass)
        {
            return Hasher.VerifyPassword(attemptPassword, correctPass);
        }

        public bool CreateUser(string username, string password)
        {
            //Check if username already exists
            if (GetDatabaseUserFromUsername(username) == null)
            {
                string hashedPass = Hasher.HashPassword(password);
                context.User.Add(new User { Username = username, Password = hashedPass });
                context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
