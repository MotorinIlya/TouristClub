namespace TouristClub;
using Microsoft.EntityFrameworkCore;
using TouristClub.Data;
using TouristClub.Models;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Подключаем PostgreSQL
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

        var app = builder.Build();

        // Эндпоинт: получаем всех туристов
        app.MapGet("/tourists", async (ApplicationDbContext db) =>
        {
            var tourists = await db.Tourists.ToListAsync();
            return Results.Ok(tourists);
        });

        app.MapPost("/tourists", async (ApplicationDbContext db, Tourists newTourist) =>
        {
            db.Tourists.Add(newTourist);
            await db.SaveChangesAsync();

            return Results.Created($"/tourists/{newTourist.id}", newTourist);
        });

        app.Run();
    }
}