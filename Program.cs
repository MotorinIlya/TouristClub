using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using TouristClub.Data;

namespace TouristClub;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Конфигурация подключения к PostgreSQL
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

        // Сервисы
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        var app = builder.Build();

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        // Запуск приложения
        app.Run();
    }
}
