using FinalProject.Persistence.ServiceRegisteratation;
using FinalProject.Application.ServiceRegisteratation;
using FinalProject.Application.Abstractions.Token;
using FinalProject.Persistence.Implementations.Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FinalProject.Infrastructure.ServiceRegisteration;
using FinalProject.Persistence.Contexts;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPersistenceServices(builder.Configuration)
                .AddApplicationServices()
                .AddInfrastructureServices(builder.Configuration);


var app = builder.Build();

var scope = app.Services.CreateScope();
var initalizer = scope.ServiceProvider.GetRequiredService<AppDbContextInitalizer>();
initalizer.InitializeDb().Wait();
initalizer.CreateRolesAsync().Wait();
initalizer.InitalizeAdminAsync().Wait();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
