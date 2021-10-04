using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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

            services.AddControllers()
                    .AddFluentValidation(_ => _.RegisterValidatorsFromAssembly(typeof(Startup).GetTypeInfo().Assembly));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "todo_app_web", Version = "v1" });
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => { });

            services.AddScoped<ITodoListRepository, TodoListRepository>();
            services.AddScoped<ITodoItemRepository, TodoItemRepository>();
            
            services.AddScoped<IAuthenticator, BCryptAuthenticator>();
            services.AddScoped<IUserRepository, UserRepository>();
            
            services.AddSingleton<ISystemClock, DefaultSystemClock>();

            services.AddScoped(_ => new SqlConnection(Configuration.GetConnectionString("TodoApp")));

            services.AddSingleton(_ => new HiLoState(10));
            services.AddScoped<IIdentityService, HiLoIdentityService>();
            services.AddScoped<IHiLoStore, SqlHiLoStore>(_ => new SqlHiLoStore(_.GetRequiredService<SqlConnection>(), "HiLoSequence"));

            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);

            services.AddDbContext<TodoDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("TodoApp")).EnableSensitiveDataLogging());
            services.AddDbContext<UserDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("TodoApp")).EnableSensitiveDataLogging());

            services.AddScoped<ITodoUnitOfWork>(_ => _.GetRequiredService<TodoDbContext>());
            services.AddScoped<IUserUnitOfWork>(_ => _.GetRequiredService<UserDbContext>());

            services.AddSingleton<IEventBus,InMemoryEventBus>();

            services.AddHttpContextAccessor();

            services.AddAntiforgery(options =>
            {
                options.HeaderName = "X-XSRF-TOKEN";                               
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "todo_app_web v1"));
            }

            app.UseStaticFiles();
            app.UseFileServer();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
