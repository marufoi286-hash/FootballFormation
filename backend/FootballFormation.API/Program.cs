using System;
using Microsoft.EntityFrameworkCore;
using FootballFormation.API.Data;
using FootballFormation.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.AddNpgsqlDbContext<ApplicationDbContext>("footballformationdb");

builder.Services.AddScoped<IPlayerService, PlayerService>();

builder.Services.AddOpenApi();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI();

    using(var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        try
        {
            // = dotnet ef database update で実行される内容と同等
            dbContext.Database.Migrate();
            Console.WriteLine("Database migration completed successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database migration failed: {ex.Message}");
        }
    }
}

// app.MapGet("/hello", () => "Hello World!");

app.UseHttpsRedirection();

app.MapControllers();

app.Run();