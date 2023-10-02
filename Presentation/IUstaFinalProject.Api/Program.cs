using FluentValidation.AspNetCore;
using IUstaFinalProject.Api;
using IUstaFinalProject.Application;
using IUstaFinalProject.Application.Validators.Agreements;
using IUstaFinalProject.Domain.Entities.Identity;
using IUstaFinalProject.Infrastructure;
using IUstaFinalProject.Infrastructure.Filters;
using IUstaFinalProject.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddPersistenceService();


builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
 policy.WithOrigins("https://localhost:7176", "http://localhost:7176").AllowAnyHeader().AllowAnyMethod()
));

Logger log = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.MSSqlServer(builder.Configuration.GetConnectionString("Default"),"logs").Enrich.FromLogContext()
    .MinimumLevel.Information()
    .CreateLogger();

builder.Host.UseSerilog(log);

builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>())
    .AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<AgreementDtoValidator>())
    .ConfigureApiBehaviorOptions(o => o.SuppressModelStateInvalidFilter = true);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwager();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Admin", options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidAudience = builder.Configuration["JWT:Audience"],
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecurityKey"])),
            LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false
        };
    });

builder.Services.AddAuthenticationAndAuthorization(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

//var container = app.Services.CreateScope();
//var userManager = container.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
//var roleManager = container.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
//if (!await roleManager.RoleExistsAsync("Admin"))
//{
//    var result = await roleManager.CreateAsync(new IdentityRole("Admin"));
//}

//var user = await userManager.FindByEmailAsync("admin@admin.com");
//if (user is null)
//{
//    user = new AppUser
//    {
//        NameSurname="admin admin",
//        UserName = "admin@admin.com",
//        Email = "admin@admin.com"
//    };
//    var result = await userManager.CreateAsync(user, "Admin");
//    result = await userManager.AddToRoleAsync(user, "Admin");
//}

//using (var scope = app.Services.CreateScope())
//{
//    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
//    var roles = new[] { "Admin", "Worker", "Customer" };
//    foreach (var role in roles)
//    {
//        if (!await roleManager.RoleExistsAsync(role))
//            await roleManager.CreateAsync(new IdentityRole(role));

//    }

//}

app.Run();

