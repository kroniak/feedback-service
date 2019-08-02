using System;
using FeedBack.Core.Database;
using FeedBack.WebApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace FeedBack.WebApi
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var authKey = _configuration["AUTHKEY"];
            var adminPass = _configuration["ADMINPASS"];
            var userPass = _configuration["USERPASS"];
            var connection = _configuration["MONGO"];
            var database = _configuration["DBNAME"] ?? "FeedBack";
            
            if (string.IsNullOrWhiteSpace(authKey)
                || string.IsNullOrWhiteSpace(adminPass)
                || string.IsNullOrWhiteSpace(userPass))
                throw new ArgumentNullException(authKey, "Secret key must be not null");

            if (string.IsNullOrWhiteSpace(connection))
                throw new ArgumentNullException(connection, "ConnectionString key must be not null");

            // configure db
            services.AddSingleton(_ => new MongoClient(connection).GetDatabase(database).Init(adminPass, userPass));

            // add repositories
            services.AddMemoryCache();
            
            services.AddCustomAuthentication(authKey);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
                app.UseHttpsRedirection();
            }
            
            app.UseAuthentication();
            app.UseCors();
            
            app.UseMvc();
        }
    }
}