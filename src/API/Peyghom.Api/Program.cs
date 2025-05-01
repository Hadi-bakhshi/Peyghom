var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi("peyghom");

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();

