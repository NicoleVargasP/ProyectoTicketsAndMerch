using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TicketsAndMerch.Core.CustomEntities;
using TicketsAndMerch.Core.Interfaces;
using TicketsAndMerch.Core.Services;
using TicketsAndMerch.Infrastructure.Data;
using TicketsAndMerch.Infrastructure.Filters;
using TicketsAndMerch.Infrastructure.Mappings;
using TicketsAndMerch.Infrastructure.Repositories;
using TicketsAndMerch.Infrastructure.Validators;

namespace TicketsAndMerch.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.Sources.Clear();
            builder.Configuration
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json",
                    optional: true, reloadOnChange: true);

            if (builder.Environment.IsDevelopment())
            {
                builder.Configuration.AddUserSecrets<Program>();
            }

            var connectionString = builder.Configuration.GetConnectionString("ConnectionSqlServer");
            builder.Services.AddDbContext<TicketsAndMerchContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddAutoMapper(typeof(MappingProfile));

            builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
            builder.Services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
            builder.Services.AddScoped<IDapperContext, DapperContext>();
            builder.Services.AddSingleton<IPasswordService, PasswordService>();

            builder.Services.AddScoped<IConcertService, ConcertService>();
            builder.Services.AddScoped<IMerchService, MerchService>();
            builder.Services.AddScoped<ITicketService, TicketService>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IBuyTicketService, BuyTicketService>();
            builder.Services.AddScoped<IBuyMerchService, BuyMerchService>();
            builder.Services.AddScoped<IUserOrderRepository, UserOrderRepository>();
            builder.Services.AddScoped<IUserOrderService, UserOrderService>();

            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<GlobalExceptionFilter>();
            }).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            }).ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<ValidationFilter>();
            });

            builder.Services.Configure<PasswordOptions>
                (builder.Configuration.GetSection("PasswordOptions"));

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new()
                {
                    Title = "Tickets and Merch API",
                    Version = "v1",
                    Description = "Documentación de Tickets & Merch - NET 9",
                    Contact = new()
                    {
                        Name = "Equipo de Desarrollo UCB",
                        Email = "desarrollo@ucb.edu.bo"
                    }
                });

                var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);

                options.EnableAnnotations();

                options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "Ingrese: Bearer {token}"
                });

                options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference
                            {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });


            builder.Services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader(),
                    new HeaderApiVersionReader("x-api-version"),
                    new QueryStringApiVersionReader("api-version")
                );
            });

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters =
                new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Authentication:Issuer"],
                    ValidAudience = builder.Configuration["Authentication:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["Authentication:SecretKey"])
                    )
                };
            });

            builder.Services.AddValidatorsFromAssemblyContaining<UserDtoValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<GetByIdRequestValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<BuyTicketDtoValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<BuyMerchDtoValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<AvailableConcertQueryFilterValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<UserOrderQueryFilterValidator>();

            builder.Services.AddScoped<IValidationService, ValidationService>();
            builder.Services.AddScoped<ISecurityServices, SecurityServices>();

            builder.Configuration.AddEnvironmentVariables();

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Tickets and Merch API v1");
                options.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
