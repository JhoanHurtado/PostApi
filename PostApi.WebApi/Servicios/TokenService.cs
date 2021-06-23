using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PostApi.Models.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PostApi.WebApi.Servicios
{
    public class TokenService
    {
        private IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerarTokent(Usuario usuario)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            string SecretKey = jwtSettings.GetValue<string>("SecretKey");
            int expiracion = jwtSettings.GetValue<int>("Expiration");
            string issuer = jwtSettings.GetValue<string>("Issure");
            string audiencia = jwtSettings.GetValue<string>("Audience");

            var key = Encoding.ASCII.GetBytes(SecretKey);
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Email, usuario.Email));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, Convert.ToString(usuario.Id)));

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audiencia,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(expiracion),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
