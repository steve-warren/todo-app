using System.Reflection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WarrenSoftware.TodoApp.Core.Domain;
using WarrenSoftware.TodoApp.Core.Infrastructure;
using WarrenSoftware.TodoApp.Modules.Todo.Domain;
using WarrenSoftware.TodoApp.Modules.Todo.Infrastructure;
using WarrenSoftware.TodoApp.Modules.Users.Domain;
using WarrenSoftware.TodoApp.Modules.Users.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;

services.AddControllers()
                .AddFluentValidation(_ => _.RegisterValidatorsFromAssembly(typeof(Program).GetTypeInfo().Assembly));

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options => { });

services.AddScoped<ITodoListRepository, TodoListRepository>();
services.AddScoped<ITodoItemRepository, TodoItemRepository>();

services.AddScoped<IAuthenticator, BCryptAuthenticator>();
services.AddScoped<IUserRepository, UserRepository>();

services.AddSingleton<WarrenSoftware.TodoApp.Core.Infrastructure.ISystemClock, DefaultSystemClock>();

services.AddScoped(_ => new SqlConnection(config.GetConnectionString("TodoApp")));

services.AddSingleton(_ => new HiLoState(10));
services.AddScoped<IIdentityService, HiLoIdentityService>();
services.AddScoped<IHiLoStore, SqlHiLoStore>();

services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).GetTypeInfo().Assembly);
});

services.AddDbContext<TodoDbContext>(options => options.UseSqlServer(config.GetConnectionString("TodoApp")).EnableSensitiveDataLogging());
services.AddDbContext<UserDbContext>(options => options.UseSqlServer(config.GetConnectionString("TodoApp")).EnableSensitiveDataLogging());

services.AddScoped<ITodoUnitOfWork>(_ => _.GetRequiredService<TodoDbContext>());
services.AddScoped<IUserUnitOfWork>(_ => _.GetRequiredService<UserDbContext>());

services.AddScoped<IEventBus, InMemoryEventBus>();

services.AddHttpContextAccessor();

services.AddAntiforgery(options =>
{
    options.HeaderName = "X-XSRF-TOKEN";
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseFileServer();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
