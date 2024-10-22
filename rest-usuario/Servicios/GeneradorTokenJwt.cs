﻿using Microsoft.IdentityModel.Tokens;
using rest_biblioteca.Modelos;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;

namespace rest_usuario.Servicios
{
    public interface IGeneradorTokenJwt
    {
        string GenerarTokenAcceso(Usuario usuario);
        string GenerarTokenRefresco();
    }

    public class GeneradorTokenJwt : IGeneradorTokenJwt
    {
        public string GenerarTokenAcceso(Usuario usuario)
        {
            string claveSecreta = Environment.GetEnvironmentVariable("ClaveSecretaJwt") ?? string.Empty;
            string tiempoExpiracion = Environment.GetEnvironmentVariable("TiempoExpiracionJwt") ?? "1";

            var claveSecretaByte = Encoding.UTF8.GetBytes(claveSecreta);

            SecurityTokenDescriptor descriptor = new()
            {
                Subject = new ClaimsIdentity([
                    new Claim(ClaimTypes.Sid, usuario.IdUsuario.ToString()),
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim(ClaimTypes.Role, "User"),
                    new Claim(ClaimTypes.Name, usuario.Nombre)
                    ]),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(tiempoExpiracion)),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(claveSecretaByte), SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityTokenHandler handler = new();
            var token = handler.CreateToken(descriptor);
            return handler.WriteToken(token);
        }

        public string GenerarTokenRefresco()
        {
            var numeroAleatorio = new byte[32];
            using var random = RandomNumberGenerator.Create();
            random.GetBytes(numeroAleatorio);
            return Convert.ToBase64String(numeroAleatorio);
        }
    }
}
