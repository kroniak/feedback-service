using System;
using System.Diagnostics.CodeAnalysis;
using FeedBack.Core.Database;
using FeedBack.Core.Database.Interfaces;
using FeedBack.Core.Database.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FeedBack.WebApi.Services
{
    [ExcludeFromCodeCoverage]
    public static class ServicesExtensions
    {
        public static void AddRepositoryAndServices(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services
                .AddSingleton<IUserRepository, UserRepository>();
        }
    }
}