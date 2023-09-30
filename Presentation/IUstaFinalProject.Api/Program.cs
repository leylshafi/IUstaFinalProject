using FluentValidation.AspNetCore;
using IUstaFinalProject.Application.Validators.Agreements;
using IUstaFinalProject.Infrastructure.Filters;
using IUstaFinalProject.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
 policy.WithOrigins("https://localhost:7176", "http://localhost:7176").AllowAnyHeader().AllowAnyMethod()
));

// Add services to the container.
builder.Services.AddPersistenceService();
builder.Services.AddControllers(options=>options.Filters.Add<ValidationFilter>())
    .AddFluentValidation(config=>config.RegisterValidatorsFromAssemblyContaining<AgreementDtoValidator>())
    .ConfigureApiBehaviorOptions(o=>o.SuppressModelStateInvalidFilter=true);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
