using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    public class TokenService
    {
        //gerar token
        public Token CreateToken(CustomIdentityUser usuario, string role)
        {
            Claim[] direitosUsuario = new Claim[]
            {
                    new Claim("username", usuario.UserName),
                    new Claim("usuario", usuario.Id.ToString()),
                    new Claim(ClaimTypes.Role, role),
            };

            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("fa8c367f1b4aee1d6feaca1018ea4dc79876febbfde37eeeda2789a7bd256370"));
            var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(claims: direitosUsuario, signingCredentials: credenciais, expires: DateTime.UtcNow.AddHours(1)); //tempo de expiração do token 1h
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return new Token(tokenString);
        }
    }
}
