using FluentValidation;
using Microsoft.EntityFrameworkCore;
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

           
            var connectionString = builder.Configuration.GetConnectionString("ConnectionSqlServer");
            builder.Services.AddDbContext<TicketsAndMerchContext>(options =>
                options.UseSqlServer(connectionString));

            
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            //  Concerts
            builder.Services.AddScoped<IConcertRepository, ConcertRepository>();
            builder.Services.AddScoped<IConcertService, ConcertService>();


            // Merch
            builder.Services.AddScoped<IMerchRepository, MerchRepository>();
            builder.Services.AddScoped<IMerchService, MerchService>();

            // Tickets
            builder.Services.AddScoped<ITicketRepository, TicketRepository>();
            builder.Services.AddScoped<ITicketService, TicketService>();

            // Payments
            builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();

            // Orders
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IOrderService, OrderService>();

            // Users
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();

            //builder.Services.AddValidatorsFromAssemblyContaining<UserDtoValidator>();
            // Servicio general de validaci�n
            builder.Services.AddScoped<IValidationService, ValidationService>();

            // Registrar todos los validadores de forma autom�tica
            builder.Services.AddValidatorsFromAssemblyContaining<ValidationService>();

            // Agregar filtro de validaci�n global
            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<ValidationFilter>();
            })
            // Evitar errores por referencias c�clicas en JSON
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling =
                    Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            })
            // Desactivar validaci�n autom�tica de ModelState (usamos FluentValidation)
            .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            var app = builder.Build();

            // Middleware: redirigir a HTTPS
            app.UseHttpsRedirection();

            // Middleware: autorizaci�n (puedes agregar autenticaci�n m�s adelante)
            app.UseAuthorization();

            // Registrar los controladores de la API
            app.MapControllers();

            // Iniciar la aplicaci�n
            app.Run();
        }
    }
}