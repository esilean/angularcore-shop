using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AngularShop.Application.Services.Security;
using AngularShop.Core.Entities.Identity;
using AngularShop.Core.Settings;
using Microsoft.IdentityModel.Tokens;

namespace AngularShop.Infra.Services.Security
{
    public class TokenService : ITokenService
    {
        private readonly ApiSettingsData _apiSettingsData;
        private readonly SymmetricSecurityKey _key;
        public TokenService(ApiSettingsData apiSettingsData)
        {
            _apiSettingsData = apiSettingsData;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_apiSettingsData.Token.Key));
        }
        public string CreateToken(AppUser appUser)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, appUser.Email),
                new Claim(ClaimTypes.GivenName, appUser.DisplayName)
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptior = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = creds,
                Issuer = _apiSettingsData.Token.Issuer,
                Audience = _apiSettingsData.Token.Audience
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptior);

            return tokenHandler.WriteToken(token);
        }
    }
}