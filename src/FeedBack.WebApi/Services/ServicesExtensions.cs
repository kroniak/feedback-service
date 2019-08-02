using System.Diagnostics.CodeAnalysis;
using System.Text;
using FeedBack.Core.Database;
using FeedBack.Core.Database.Interfaces;
using FeedBack.WebApi.Services.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace FeedBack.WebApi.Services
{
    [ExcludeFromCodeCoverage]
    public static class ServicesExtensions
    {
        public static IServiceCollection AddRepositoryAndServices(this IServiceCollection services)
        {
            services
                .AddSingleton<IUserRepository, UserRepository>()
                .AddSingleton<ISimpleAuthenticateService, SimpleAuthenticateService>();

            return services;
        }

        /// <summary>
        /// Add custom Auth middleware
        /// </summary>
        /// <param name="services">IService Collections</param>
        /// <param name="key">Global key configuration</param>
        public static void AddCustomAuthentication(this IServiceCollection services, string key)
        {
            var keyEncoded = Encoding.ASCII.GetBytes(key);
            services
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
    }
}