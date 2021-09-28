using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolApi.Interfaces;
using SchoolApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class LoginController : Controller, IHaveDbContext
    {
        private DbContext context;

        public DbContext Context
        {
            get
            {
                return context;
            }
            set
            {
                context = value;
            }
        }
        public LoginController(DbContext context)
        {
            Context = context;
        }


        [HttpPost]
        public bool Login([FromBody] User userFromFrom)
        {
            try
            {
                UserManager manager = new UserManager((SchoolContext)Context);
                User dbUser = manager.GetDatabaseUserFromUsername(userFromFrom.Username);

                if (dbUser == null)
                    return false;

                return manager.VerifyLogin(userFromFrom.Password, dbUser.Password);

            }
            catch (Exception)
            {
                return false;
            }
        }

        [HttpPost]
        [Route("Register")]
        [ApiKeyAuth(key = "Register")]
        public IActionResult CreateUser(User user)
        {
            try
            {
                UserManager manager = new UserManager((SchoolContext)Context);

                if (manager.CreateUser(user.Username, user.Password))
                    return Ok(user.Username + " was created successfully");
                else
                    return BadRequest("That username is already taken");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal server error. Something went wrong\n" + e.Message);
            }
        }
    }
}
