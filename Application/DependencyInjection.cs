using System.Text;
using Application.Services;
using Domain.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {

            // MediatR
            var assembly = typeof(DependencyInjection).Assembly;
            services.AddMediatR(configuration =>
                configuration.RegisterServicesFromAssembly(assembly));

            // AutoMapper
            services.AddAutoMapper(assembly);

            // Jwt
            var secret = configuration["AppSettings:Secret"] ?? "mysupersecretsecret";
            var issuer = configuration["AppSettings:JwtIssuer"] ?? "*";
            var audience = configuration["AppSettings:JwtAudience"] ?? "*";

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)), // Cambia esto
                };

            });

            // services
            services.AddSingleton<IJwtService>(new JwtService(secret, issuer, audience));


            return services;
        }
    }
}
