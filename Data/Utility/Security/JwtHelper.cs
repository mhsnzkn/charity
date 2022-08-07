using Data.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Data.Utility.Security
{
    public class JwtHelper : ITokenHelper
    {
        public IConfiguration Configuration { get; }
        private TokenOptions tokenOptions;
        private DateTime _accessTokenExpiration;

        public JwtHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
        }
        public AccessToken CreateToken(User user)
        {
            _accessTokenExpiration = DateTime.Now.AddDays(tokenOptions.AccessTokenExpiration);
            var securityKey = SecurityHelper.CreateSecurityKey(tokenOptions.SecurityKey);
            var signingCredetials = SecurityHelper.CreateSigningCredentials(securityKey);
            var jwt = CreateJwtSecurityToken(tokenOptions, user, signingCredetials);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken()
            {
                Token = token,
                Role = user.Role,
                VolunteerId = user.VolunteerId ?? 0
            };
        }

        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user, SigningCredentials signingCredentials)
        {
            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(user),
                signingCredentials: signingCredentials
                );
            return jwt;
        }

        private IEnumerable<Claim> SetClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            };
            return claims;
        }
    }
}
