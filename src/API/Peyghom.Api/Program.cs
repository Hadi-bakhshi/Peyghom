using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Peyghom.Api.Middleware;
using Peyghom.Common;
using Peyghom.Modules.Users;
using Scalar.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfiguration) =>
    loggerConfiguration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi("peyghom");

builder.Services.AddApplication([Peyghom.Modules.Users.AssemblyReference.Assembly]);

string databaseConnectionString = builder.Configuration.GetConnectionString("Database")!;
string redisConnectionString = builder.Configuration.GetConnectionString("Cache")!;

builder.Services.AddInfrastructure(
    databaseConnectionString,
    redisConnectionString);


builder.Services.AddHealthChecks();
    

//builder.Configuration.AddModuleConfiguration(["messaging", "users", "calling"]);

builder.Services.AddUsersModule(builder.Configuration);


var app = builder.Build();
    app.MapOpenApi();

if (app.Environment.IsDevelopment())
{
}
app.MapScalarApiReference(options =>
{
    options.WithTitle("Peyghom");
    options.AddServer(new ScalarServer("https://localhost:5001"));
    options.AddServer(new ScalarServer("http://localhost:5000"));
    options.AddServer(new ScalarServer("http://localhost:8080"));
    options.AddServer(new ScalarServer("https://localhost:8081"));
}); // scalar/v1

//app.UseHttpsRedirection();

app.MapHealthChecks("health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseLogContextTraceLogging();

app.UseSerilogRequestLogging();

app.UseExceptionHandler();

//app.UseAuthentication();

//app.UseAuthorization();

app.MapEndpoints();



app.Run();

