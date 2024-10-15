using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rest_biblioteca.Dependencias
{
    public static class JwtDependencia
    {
        public static IServiceCollection AgregarJwtTokenValidacion(this IServiceCollection service)
        {
            string claveSecreta = Environment.GetEnvironmentVariable("ClaveSecretaJwt") ?? string.Empty;
            var claveSecretaByte = Encoding.UTF8.GetBytes(claveSecreta);

            service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(option =>
                {
                    option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(claveSecretaByte)
                    };
                });

            return service;
        }
    }
}
