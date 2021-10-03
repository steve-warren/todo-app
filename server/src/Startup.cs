using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using WarrenSoftware.TodoApp.Core.Domain;
using WarrenSoftware.TodoApp.Core.Infrastructure;
using WarrenSoftware.TodoApp.Modules.Todo.Domain;
using WarrenSoftware.TodoApp.Modules.Todo.Infrastructure;
using WarrenSoftware.TodoApp.Modules.Users.Domain;
using WarrenSoftware.TodoApp.Modules.Users.Infrastructure;

namespace WarrenSoftware.TodoApp.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "todo_app_web", Version = "v1" });
            });

            services.AddScoped<ITodoListIdentityService, InMemoryTodoListIdentityService>();
            services.AddScoped<ITodoItemIdentityService, InMemoryTodoItemIdentityService>();
            services.AddScoped<ITodoListRepository, InMemoryTodoListRepository>();
            services.AddScoped<ITodoItemRepository, InMemoryTodoItemRepository>();
            
            services.AddScoped<IAuthenticator, BCryptAuthenticator>();
            services.AddScoped<IUserRepository, InMemoryUserRepository>();
            
            services.AddScoped<IUnitOfWork, InMemoryUnitOfWork>();
            services.AddSingleton<ISystemClock, DefaultSystemClock>();

            services.AddScoped(_ => new SqlConnection(Configuration.GetConnectionString("TodoApp")));

            services.AddSingleton(_ => new HiLoGeneratorState(10));
            services.AddScoped<HiLoGenerator>();
            services.AddScoped<IHiLoRepository, SqlHiLoRepository>(_ => new SqlHiLoRepository(_.GetRequiredService<SqlConnection>(), "HiLoSequence"));

            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "todo_app_web v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
