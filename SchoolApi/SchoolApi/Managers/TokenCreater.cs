using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SchoolApi.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApi.Managers
{
    public class TokenCreater
    {

        private IConfiguration config;
        public IConfiguration Config
        {
            get { return config; }
            set { config = value; }
        }
        public TokenCreater(IConfiguration config)
        {
            Config = config;
        }

        /// <summary>
        /// Creates a new <see cref="IssuedToken"/> object with a newly generated token string
        /// </summary>
        /// <param name="username">Username of the user</param>
        /// <param name="expDate"> When the token should expire</param>
        /// <returns></returns>
        public IssuedToken CreateToken(string username, DateTime expDate)
        {
            return new IssuedToken
            {
                Username = username,
                TokenString = GenerateJWTToken(username, expDate),
                ExpiryDate = expDate
            };
        }

        /// <summary>
        /// Generate a Json Web token, including the username in the payload
        /// </summary>
        /// <param name="username"></param>
        /// <param name="expiryDate"></param>
        /// <returns></returns>
        private string GenerateJWTToken(string username, DateTime expiryDate)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config["Jwt:Key"]));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(Config["Jwt:Issuer"],
              Config["Jwt:Issuer"],
              null,
              expires: expiryDate,
              signingCredentials: credentials);

            token.Payload["name"] = username;

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Extend a valid token's expiry date with 15 minutes
        /// </summary>
        /// <param name="token"></param>
        public void RefreshToken(IssuedToken token)
        {
            DateTime expDate = DateTime.Now.AddMinutes(15);
            token.TokenString = GenerateJWTToken(token.Username, expDate);
            token.ExpiryDate = expDate;
        }
    }
}
