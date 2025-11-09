using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TicketsAndMerch.Core.Interfaces;
using TicketsAndMerch.Core.Services;
using TicketsAndMerch.Infrastructure.Data;
using TicketsAndMerch.Infrastructure.Filters;
using TicketsAndMerch.Infrastructure.Mappings;
using TicketsAndMerch.Infrastructure.Repositories;
using TicketsAndMerch.Infrastructure.Validators;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("ConnectionSqlServer");
builder.Services.AddDbContext<TicketsAndMerchContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddAutoMapper(typeof(MappingProfile));


builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();


builder.Services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
builder.Services.AddScoped<IDapperContext, DapperContext>();


builder.Services.AddScoped<IConcertService, ConcertService>();
builder.Services.AddScoped<IMerchService, MerchService>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IUserService, UserService>();


builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>();
    options.Filters.Add<GlobalExceptionFilter>();
})
.AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling =
        Newtonsoft.Json.ReferenceLoopHandling.Ignore;
})
.ConfigureApiBehaviorOptions(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddValidatorsFromAssemblyContaining<UserDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<GetByIdRequestValidator>();

// Servicio general de validaciones
builder.Services.AddScoped<IValidationService, ValidationService>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    {
        Title = "Tickets and Merch API",
        Version = "v1",
        Description = "Documentación de la API de Tickets & Merch - NET 9",
        Contact = new()
        {
            Name = "Equipo de Desarrollo UCB",
            Email = "desarrollo@ucb.edu.bo"
        }
    });

    // Incluye los comentarios XML (para SwaggerDoc)
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
    options.EnableAnnotations();
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Tickets and Merch API v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();


app.Run();
