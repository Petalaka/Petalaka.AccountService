using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Petalaka.Account.API;
using Petalaka.Account.API.Middleware;
using Petalaka.Account.Contract.Repository.Entities;
using Petalaka.Account.Repository;
using Petalaka.Account.Repository.Base;
using Petalaka.Account.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Configuration.AddEnvironmentVariables();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
var infrastructureConfig = builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();


builder.Services.AddHttpContextAccessor();

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    
}).AddEntityFrameworkStores<PetalakaDbContext>()
.AddSignInManager<SignInManager<ApplicationUser>>()
.AddDefaultTokenProviders(); 

builder.Services.AddConfigureServiceRepository(builder.Configuration);
builder.Services.AddConfigureServiceService(builder.Configuration);
builder.Services.AddConfigureServiceAPI(builder.Configuration);
var app = builder.Build();
app.UseCors("AllowSpecificOrigin");
app.UseHttpsRedirection();

if (app.Environment.IsProduction())
{
    app.Use((context, next) =>
    {
        context.Request.Scheme = "https";
        return next(context);
    });
}
await app.UseInitializeDatabaseAsync();

app.UseRouting();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowGoogle");
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Service API v1");
});
app.UseMiddleware<CustomExceptionHandlerMiddleware>();
app.UseMiddleware<ValidateJwtTokenMiddleware>();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor |
                       ForwardedHeaders.XForwardedProto
});

app.MapControllers();
app.Run();
