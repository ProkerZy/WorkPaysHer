using Microsoft.EntityFrameworkCore;
using WorkPaysHer.Models;
using WorkPaysHer.Services;

namespace WorkPaysHer
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddScoped<IPayrollService, PayrollCalculator>();
            builder.Services.AddScoped<IShiftHistoryService, ShiftHistoryService>();
            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql("Host=localhost;Database=MacPayDb;Username=postgres;Password=228555"));
            builder.Services.AddCors();

            var app = builder.Build();
            // Автоматическое наполнение базы при запуске
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>(); // получает наш AppDbContext
                if (!db.Positions.Any())
                {
                    db.Positions.AddRange(
                    new Position { PositionName = "Менеджер", RatePerHour = 280m },
                    new Position { PositionName = "Директор", Salary = 80.000m },
                    new Position { PositionName = "Лидер департамента", Salary = 65.000m },
                    new Position { PositionName = "Ассистент директора", Salary = 75.000m },
                    new Position { PositionName = "Работник", RatePerHour = 189 },
                    new Position { PositionName = "Инструктор", RatePerHour = 204m },
                    new Position { PositionName = "Ночник", RatePerHour = 209m },
                    new Position { PositionName = "Инструктор ночной смены", RatePerHour = 250m },
                    new Position { PositionName = "Техник", Salary = 60.800m }
                    );
                    db.SaveChanges();
                }
                if(!db.Friends.Any())
                {
                    db.Friends.AddRange(
                        new Friend { Name = "Андрей" },
                        new Friend { Name = "Даниил" },
                        new Friend { Name = "Рома" },
                        new Friend { Name = "Савелий" },
                        new Friend { Name = "Настюха" },
                        new Friend { Name = "Карина" });
                    db.SaveChanges();
                };
            }

            // Configure the HTTP request pipeline.
            app.UseCors(policy =>
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader());
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseStaticFiles();
            app.MapControllers();
            app.MapFallbackToFile("index.html");
            app.Run();
        }
    }
}
