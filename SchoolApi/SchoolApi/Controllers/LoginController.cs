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
        public IActionResult Login([FromBody] User userFromFrom)
        {
            try
            {
                UserManager manager = new UserManager((SchoolContext)Context);
                User dbUser = manager.GetDatabaseUserFromUsername(userFromFrom.Username);

                if (dbUser == null)
                    return NotFound($"Login was not successful, [{userFromFrom.Username}] does not exist");

                if (manager.VerifyLogin(userFromFrom.Password, dbUser.Password))
                    return Ok($"Login was successful! Welcome {userFromFrom.Username}");
                else
                    return Unauthorized("Password is wrong, try again!");

            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong, please try again!\n" + e.Message);
            }
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult CreateUser(User user)
        {
            try
            {
                UserManager manager = new UserManager((SchoolContext)Context);

                if (manager.CreateUser(user.Username, user.Password))
                    return Ok();
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
