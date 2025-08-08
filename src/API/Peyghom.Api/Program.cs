using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Peyghom.Api.Middleware;
using Peyghom.Common;
using Peyghom.Modules.Chat;
using Peyghom.Modules.Users;
using Scalar.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfiguration) =>
    loggerConfiguration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddCors(option =>
{
    option.AddPolicy("cors", policy =>
    {
        policy.WithOrigins("http://localhost:3000");
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowCredentials();
        policy.SetIsOriginAllowed(host => true);
    });
});

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

builder.Services.AddApplication([Peyghom.Modules.Users.AssemblyReference.Assembly]);

string databaseConnectionString = builder.Configuration.GetConnectionString("Database")!;
string redisConnectionString = builder.Configuration.GetConnectionString("Cache")!;

builder.Services.AddInfrastructure(
    databaseConnectionString,
    redisConnectionString);


builder.Services.AddHealthChecks();


//builder.Configuration.AddModuleConfiguration(["messaging", "users", "calling"]);

builder.Services.AddUsersModule(builder.Configuration);
builder.Services.AddChatModule(builder.Configuration);

var app = builder.Build();


await app.Services.SeedUsersDatabaseAsync();

app.MapOpenApi();

app.UseStatusCodePages();

app.MapScalarApiReference(options =>
{
    options.WithTitle("Peyghom");
}); // scalar/v1

app.UseExceptionHandler();

app.UseSerilogRequestLogging();

app.UseLogContextTraceLogging();

app.UseCors("cors");

app.UseAuthentication();

app.UseAuthorization();


app.MapHealthChecks("health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapEndpoints();

app.Run();

