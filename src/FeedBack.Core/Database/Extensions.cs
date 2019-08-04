using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace FeedBack.Core.Database
{
    [ExcludeFromCodeCoverage]
    public static class Extensions
    {
        public static void AddMongoDb(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            var connection = configuration.GetSection("Database")["DB_HOST"] ??
                             throw new ArgumentException("configuration.GetSection(\"Database\")[\"DB_HOST\"]");
            var database = configuration.GetSection("Database")["DB_NAME"] ?? "FeedBack";

            var adminPass = configuration.GetSection("Security")["ADMIN_PASSWORD"] ??
                            throw new ArgumentException(
                                "configuration.GetSection(\"Security\")[\"ADMIN_PASSWORD\"]");
            var userPass = configuration.GetSection("Security")["USER_PASSWORD"] ??
                           throw new ArgumentException("configuration.GetSection(\"Security\")[\"USER_PASSWORD\"]");

            var mongo = new MongoClient(connection).GetDatabase(database).Init(adminPass, userPass);
            services.AddSingleton(mongo);
        }
    }
}