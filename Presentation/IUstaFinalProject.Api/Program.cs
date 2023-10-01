using FluentValidation.AspNetCore;
using IUstaFinalProject.Application;
using IUstaFinalProject.Application.Validators.Agreements;
using IUstaFinalProject.Infrastructure;
using IUstaFinalProject.Infrastructure.Filters;
using IUstaFinalProject.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddApplicationServices();
        builder.Services.AddInfrastructureServices();
        builder.Services.AddPersistenceService();
        
        builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
         policy.WithOrigins("https://localhost:7176", "http://localhost:7176").AllowAnyHeader().AllowAnyMethod()
        ));


        builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>())
            .AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<AgreementDtoValidator>())
            .ConfigureApiBehaviorOptions(o => o.SuppressModelStateInvalidFilter = true);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddAuthentication("Admin")
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidAudience = builder.Configuration["JWT:Audience"],
                    ValidIssuer = builder.Configuration["JWT:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecurityKey"]))
                };
            });
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseCors();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}