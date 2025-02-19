using FinalProject.Persistence.ServiceRegisteratation;
using FinalProject.Application.ServiceRegisteratation;
using FinalProject.Application.Abstractions.Token;
using FinalProject.Persistence.Implementations.Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FinalProject.Infrastructure.ServiceRegisteration;
using FinalProject.Persistence.Contexts;
using Microsoft.OpenApi.Models;
using FinalProject.Domain.Entities;
using Stripe;
using System.Text.Json.Serialization;




internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddPersistenceServices(builder.Configuration)
                        .AddApplicationServices()
                        .AddInfrastructureServices(builder.Configuration);




        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "FinalProject", Version = "v1" });

            // JWT Auth i�in Bearer Token Ayar�
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Token ile Authentication (�rnek: 'Bearer {token}')",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
            });
        });


        builder.Services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowReactApp",
                builder => builder.WithOrigins("http://localhost:3000")
                                  .AllowAnyMethod()
                                  .AllowAnyHeader());
        });

        builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));

        var app = builder.Build();
        app.UseAuthentication();
        app.UseAuthorization();
        var scope = app.Services.CreateScope();
        var initalizer = scope.ServiceProvider.GetRequiredService<AppDbContextInitalizer>();
        StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];
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
    }
}