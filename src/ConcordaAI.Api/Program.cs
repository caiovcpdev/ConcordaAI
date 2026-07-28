using Asp.Versioning;
using ConcordaAI.Api.Middlewares;
using ConcordaAI.Application.DTOs.Auth;
using ConcordaAI.Application.DTOs.Eventos;
using ConcordaAI.Application.Interfaces;
using ConcordaAI.Application.Services;
using ConcordaAI.Application.Validators.Eventos;
using ConcordaAI.Application.Validators.Trabalhadores;
using ConcordaAI.Application.Validators.Usuarios;
using ConcordaAI.Domain.Interfaces;
using ConcordaAI.Infrastructure.Persistence.Connection;
using ConcordaAI.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);


//Serilog configs
Log.Logger = new LoggerConfiguration()
                 .MinimumLevel.Information()
                 .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                 .Enrich.FromLogContext()
                 .Enrich.WithMachineName()
                 .Enrich.WithThreadId()
                 .WriteTo.Console()
                 .WriteTo.File(
                    "logs/log-.txt",
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 30)
                 .CreateLogger();

builder.Host.UseSerilog();


//HealthChecks configs
builder.Services.AddHealthChecks()
    .AddSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")!,
        name: "sqlserver",
        failureStatus: HealthStatus.Unhealthy);

builder.Services.AddControllers();

builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
});


builder.Services.AddScoped<IDbConnectionFactory, SqlConnectionFactory>();

builder.Services.AddScoped<IEventoRepository, EventoRepository>();
builder.Services.AddScoped<ITrabalhadorRepository, TrabalhadorRepository>();
builder.Services.AddScoped<IEventoTrabalhadorRepository, EventoTrabalhadorRepository>();
builder.Services.AddScoped<IEscalaRepository, EscalaRepository>();
builder.Services.AddScoped<ICheckInRepository, CheckInRepository>();
builder.Services.AddScoped<IOcorrenciaRepository, OcorrenciaRepository>();
builder.Services.AddScoped<IPagamentoRepository, PagamentoRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddScoped<IEventoService, EventoService>();
builder.Services.AddScoped<ITrabalhadorService, TrabalhadorService>();
builder.Services.AddScoped<IEventoTrabalhadorService, EventoTrabalhadorService>();
builder.Services.AddScoped<IEscalaService, EscalaService>();
builder.Services.AddScoped<ICheckInService, CheckInService>();
builder.Services.AddScoped<IOcorrenciaService, OcorrenciaService>();
builder.Services.AddScoped<IPagamentoService, PagamentoService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IValidator<CriarEventoRequest>, CriarEventoRequestValidator>();
builder.Services.AddScoped<IValidator<CriarTrabalhadorRequest>, CriarTrabalhadorRequestValidator>();
builder.Services.AddScoped<IValidator<CriarUsuarioRequest>, CriarUsuarioRequestValidator>();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
});

var jwtSettings = builder.Configuration.GetSection("Jwt");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtSettings["Key"]!))
    };
});


var app = builder.Build();

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

//Mapeando endpoint de healthcheck
app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = async (context, report) =>
    {
        context.Response.ContentType = "application/json";

        var response = new
        {
            status = report.Status.ToString(),
            checks = report.Entries.Select(x => new
            {
                component = x.Key,
                status = x.Value.Status.ToString(),
                description = x.Value.Description
            }),
            totalDuration = report.TotalDuration.TotalMilliseconds
        };

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(response));
    }
});

app.Run();