using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Peliculas.Services
{
    public class AuthService : IAuthService
    {
        AuthOptions Options;

        public AuthService(IOptions<AuthOptions> options)
        {
            Options = options.Value;
        }
        public bool ValidateUser(string username, string password)
        {
            return true;
        }
        public string GenerateAccessToken(DateTime now, string username, TimeSpan validtime)
        {
            var expires = now.Add(validtime);
            var claims = new Claim[]
            {
                    new Claim(JwtRegisteredClaimNames.Sub, username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(
                        JwtRegisteredClaimNames.Iat,
                        new DateTimeOffset(now).ToUniversalTime().ToUnixTimeSeconds().ToString(),
                        ClaimValueTypes.Integer64
                    ),
                    new Claim(
                        "roles",
                        "ADMIN"
                    )
            };
            var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Options.SigningKey)),
                SecurityAlgorithms.HmacSha256Signature
            );
            var jwt = new JwtSecurityToken(
                issuer: "PeliculasAPI",
                audience: "Public",
                claims: claims,
                notBefore: now,
                expires: expires,
                signingCredentials: signingCredentials
            );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;
        }
    }

    public interface IAuthService {
        bool ValidateUser(string username, string password);
        string GenerateAccessToken(DateTime now, string username, TimeSpan validtime);
    }
}