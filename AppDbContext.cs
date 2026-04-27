using Microsoft.EntityFrameworkCore;
using WorkPaysHer.Models;

namespace WorkPaysHer
{
    public class AppDbContext : DbContext
    {
        // 2. Конструктор. 
        // Сюда в будущем прилетят настройки подключения из Program.cs.
        // Пока просто запомни эту строку, она стандартная.
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Принудительно указываем PostgreSQL использовать тип БЕЗ таймзоны для DateTime
            modelBuilder.Entity<CalcShift>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("timestamp without time zone");
                entity.Property(e => e.StartTime).HasColumnType("timestamp without time zone");
                entity.Property(e => e.EndTime).HasColumnType("timestamp without time zone");
            });
        }
        // 3. Наборы данных (DbSet).
        // Каждый DbSet = Одна таблица в базе данных.
        public DbSet<Position> Positions { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<CalcShift> CalcShifts { get; set; }
    }
}