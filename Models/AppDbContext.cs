using Microsoft.EntityFrameworkCore;

namespace TouristClub.Models;

public class AppDbContext : DbContext
{
    public DbSet<Users> People { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Users>().ToTable("Users");
        modelBuilder.Entity<Tourists>().ToTable("Tourists");
    }
}