
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SchoolApi.Interfaces;
using SchoolApi.Models;
using System;
using System.Linq;


namespace SchoolApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class LoginController : Controller, IHaveDbContext
    {
        private UserManager userManager;
        private TokenManager tokenManager;
        private IConfiguration config;

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

        //Di of context and config
        public LoginController(DbContext context, IConfiguration config)
        {
            Context = context;
            this.config = config;
        }


        [HttpPost]
        public IActionResult Login([FromBody] User userFrom)
        {
            try
            {
                using (userManager = new UserManager((SchoolContext)Context))
                {
                    User dbUser = userManager.GetDatabaseUserFromUsername(userFrom.Username);

                    if (dbUser != null)
                    {

                        if (userManager.VerifyLogin(userFrom.Password, dbUser.Password))
                        {
                            tokenManager = new TokenManager(config);
                            DateTime expDate = DateTime.Now.AddMinutes(15);

                            IssuedToken token = tokenManager.CreateToken(userFrom.Username, expDate);

                            ((SchoolContext)Context).IssuedToken.Add(token);
                            ((SchoolContext)Context).SaveChanges();
                            return Ok(new { Token = token.TokenString });
                        }
                    }
                    return Unauthorized();
                }
            }
            catch (ArgumentNullException e)
            {
                return BadRequest(e.Message);
            }
        }



        /// <summary>
        /// Get the username of a user, specified by a token they're holding
        /// </summary>
        /// <param name="tokenString">The token value the user is holding</param>
        /// <returns>The username of the token holder</returns>
        [HttpPost]
        [Route("IsLoggedIn")]
        public IActionResult IsLoggedIn(string tokenString)
        {
            try
            {
                IssuedToken userToken = ((SchoolContext)Context).IssuedToken.Where(token => token.TokenString == tokenString).FirstOrDefault();
                if (userToken != null)
                {
                    tokenManager = new TokenManager(config);
                    tokenManager.RefreshToken(userToken);

                    Context.SaveChanges();
                    return Ok(new { Token = userToken.TokenString, User = userToken.Username });
                }
                return Unauthorized("The token string is not recognized");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("Register")]
        [ApiKeyAuth(key = "Register")]
        public IActionResult CreateUser(User user)
        {
            using (UserManager manager = new UserManager((SchoolContext)Context))
            {
                try
                {
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
}
