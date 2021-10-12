using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SchoolApi.Attributes;
using SchoolApi.Interfaces;
using SchoolApi.Managers;
using SchoolApi.Models;
using System;
using System.Linq;


namespace SchoolApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //controller for handling login requests and token releases
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

        private IConfiguration config;
        public IConfiguration Config
        {
            get
            {
                return config;
            }

            set
            {
                config = value;
            }
        }
        
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

        private TokenCreater tokenManager;
        public TokenCreater TokenManager
        {
            get
            {
                return tokenManager;
            }

            set
            {
                tokenManager = value;
            }
        }



        //Di of context and config
        public LoginController(DbContext context, IConfiguration config)
        {
            Context = context;
            this.Config = config;
        }

        /// <summary>
        /// try to login a user, returns a token if successfully logged in
        /// </summary>
        /// <param name="attemptedUser">potential User the client is trying to log in to</param>
        /// <returns>Returns an Ok actionresult with a token if verified.
        /// Otherwise 403 forbidden or 400 bad request</returns>
        [HttpPost]
        public IActionResult Login([FromBody] User attemptedUser)
        {
            try
            {
                UserManager = new UserManager((SchoolContext)Context);

                User dbUser = UserManager.GetDatabaseUserFromUsername(attemptedUser.Username);

                //If the user does exist, try to login
                if (dbUser != null)
                {
                    if (UserManager.VerifyLogin(attemptedUser.Password, dbUser.Password))
                    {
                        TokenManager = new TokenCreater(Config);
                        DateTime expDate = DateTime.Now.AddMinutes(15);

                        IssuedToken token = TokenManager.CreateToken(attemptedUser.Username, expDate);

                        ((SchoolContext)Context).IssuedToken.Add(token);
                        ((SchoolContext)Context).SaveChanges();
                        return Ok(new { Token = token.TokenString });
                    }
                }
                return Forbid();
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
        [Route("IsLoggedIn")]
        public IActionResult IsLoggedIn(string tokenString)
        {
            try
            {
                IssuedToken userToken = ((SchoolContext)Context).IssuedToken
                    .Where(token => token.TokenString == tokenString)
                    .FirstOrDefault();

                if (userToken != null)
                {
                    TokenManager = new TokenCreater(Config);
                    TokenManager.RefreshToken(userToken);

                    Context.SaveChanges();
                    return Ok(new { Token = userToken.TokenString, User = userToken.Username });
                }
                else
                    return new BadRequestResult();
            }
            catch (Exception)
            {
                return new BadRequestResult();
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
            UserManager = new UserManager((SchoolContext)Context);
            try
            {
                if (UserManager.CreateUser(user.Username, user.Password))
                    return Ok(user.Username + " was created successfully");
                else
                    return new BadRequestResult();
            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal server error. Something went wrong\n" + e.Message);
            }
        }

        /// <summary>
        /// Method for logging out a user, by deleting their ongoing token from the database
        /// </summary>
        /// <param name="tokenString"></param>
        /// <returns>Returns ok if deleted successfully, otherwise returns a <see cref="BadRequestResult"/></returns>
        [HttpDelete]
        [Route("Logout")]
        public IActionResult LogOut(string tokenString)
        {
            try
            {
                IssuedToken token = ((SchoolContext)Context).IssuedToken
                             .Where(token => token.TokenString == tokenString)
                             .FirstOrDefault();
                ((SchoolContext)Context).IssuedToken.Remove(token);
                Context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return new BadRequestResult();
            }
        }

    }
}
