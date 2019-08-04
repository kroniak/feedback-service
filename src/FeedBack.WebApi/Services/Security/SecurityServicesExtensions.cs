using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace FeedBack.WebApi.Services.Security
{
    [ExcludeFromCodeCoverage]
    public static class SecurityServicesExtensions
    {
        /// <summary>
        /// Add custom Auth middleware
        /// </summary>
        /// <param name="services">IService Collections</param>
        /// <param name="configuration">Configuration root</param>
        public static void AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            var keyEncoded = GetKeyEncoded(configuration);

            services
                .AddSingleton<ISimpleAuthenticateService, SimpleAuthenticateService>()
                .AddAuthorization(options =>
                {
                    options.AddPolicy("Administrators",
                        policy => { policy.RequireRole("Administrator"); });
                    options.AddPolicy("Users",
                        policy => { policy.RequireRole("User"); });
                    options.AddPolicy("Reviewers",
                        policy => { policy.RequireRole("Reviewer"); });
                    options.AddPolicy("Editors",
                        policy => { policy.RequireRole("Editor"); });
                })
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(keyEncoded),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }

        public static byte[] GetKeyEncoded(IConfiguration configuration)
        {
            var authKey = configuration.GetSection("Security")["AUTH_KEY"];

            if (string.IsNullOrWhiteSpace(authKey))
                throw new ArgumentNullException(authKey, "Secret key must be not null");

            var keyEncoded = Encoding.ASCII.GetBytes(authKey);
            return keyEncoded;
        }
    }
}