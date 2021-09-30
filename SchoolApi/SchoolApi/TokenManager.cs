using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SchoolApi.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApi
{
    public class TokenManager
    {

        private IConfiguration config;
        public IConfiguration Config
        {
            get { return config; }
            set { config = value; }
        }
        public TokenManager(IConfiguration config)
        {
            Config = config;
        }

        public IssuedToken CreateToken(string username, DateTime expDate)
        {
            return new IssuedToken
            {
                Username = username,
                TokenString = GenerateJWTToken(expDate),
                ExpiryDate = expDate
            };
        }

        private string GenerateJWTToken(DateTime expiryDate)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config["Jwt:Key"]));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(Config["Jwt:Issuer"],
              Config["Jwt:Issuer"],
              null,
              expires: expiryDate,
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public void RefreshToken(IssuedToken token)
        {
            DateTime expDate = DateTime.Now.AddMinutes(15);
            token.TokenString = GenerateJWTToken(expDate);
            token.ExpiryDate = expDate;
        }
    }
}
