using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class TokenService : ITokenService
    {
        private  readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }
        public string CreateToken(AppUser user)
        {
            // Claim information would like to pass along JWT token
           var claims = new List<Claim>{
              // new Claim(JwtRegisteredClaimNames.NameId, user.UserName)
               new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
               new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
           };
            // Signature to sign a contract. Key is a secret
           var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            // create a page contract object: Subject, Expires, how to Sign
           var tokenDescriptor = new SecurityTokenDescriptor(){
               Subject= new ClaimsIdentity(claims),
               Expires =  DateTime.Now.AddDays(7),
               SigningCredentials = creds
           };
            // how handle to write out the contract
           var tokenHandler = new JwtSecurityTokenHandler();
            // create a contract
           var token = tokenHandler.CreateToken(tokenDescriptor);
            // transfer 
           return tokenHandler.WriteToken(token);
        }
    }
}