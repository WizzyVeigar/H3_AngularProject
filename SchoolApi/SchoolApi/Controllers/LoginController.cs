using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SchoolApi.Interfaces;
using SchoolApi.Managers;
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
        public UserManager UserManager
        {
            get
            {
                return userManager;
            }

            set
            {
                userManager = value;
            }
        }

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

        /// <summary>
        /// try to login a user, returns a token if successfully logged in
        /// </summary>
        /// <param name="attemptedUser">potential User the client is trying to log in to</param>
        /// <returns>Returns an Ok actionresult with a token if verified. Otherwise 401 unauthorized or 400 bad request</returns>
        [HttpPost]
        public IActionResult Login([FromBody] User attemptedUser)
        {
            try
            {
                using (UserManager = new UserManager((SchoolContext)Context))
                {
                    User dbUser = UserManager.GetDatabaseUserFromUsername(attemptedUser.Username);

                    if (dbUser != null)
                    {
                        if (UserManager.VerifyLogin(attemptedUser.Password, dbUser.Password))
                        {
                            tokenManager = new TokenManager(config);
                            DateTime expDate = DateTime.Now.AddMinutes(15);

                            IssuedToken token = tokenManager.CreateToken(attemptedUser.Username, expDate);

                            ((SchoolContext)Context).IssuedToken.Add(token);
                            ((SchoolContext)Context).SaveChanges();
                            return Ok(new { Token = token.TokenString });
                        }
                    }
                    return Forbid();
                }
            }
            catch (ArgumentNullException e)
            {
                return BadRequest(e.Message);
            }
        }



        /// <summary>
        /// Get the username and updated token of a user, specified by a token they're holding, 
        /// refreshes the token if they're holding a valid one
        /// </summary>
        /// <param name="tokenString">The token value the user is holding</param>
        /// <returns>The username of the token holder</returns>
        [HttpPost]
        //[JwtAuthorize]
        [Route("IsLoggedIn")]
        //Maybe change name to RefreshToken?
        public IActionResult IsLoggedIn(string tokenString)
        {
            try
            {
                IssuedToken userToken = ((SchoolContext)Context).IssuedToken
                    .Where(token => token.TokenString == tokenString)
                    .FirstOrDefault();

                if (userToken != null)
                {
                    tokenManager = new TokenManager(config);
                    tokenManager.RefreshToken(userToken);

                    Context.SaveChanges();
                    return Ok(new { Token = userToken.TokenString, User = userToken.Username });
                }
                return Forbid("The token string is not recognized");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        /// <summary>
        /// Register a user to the EF database. Must have the Register key in the header
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Register")]
        [ApiKeyAuth(Key = "Register")]
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
