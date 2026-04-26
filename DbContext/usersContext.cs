// dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL

using Microsoft.EntityFrameworkCore;

public class UsersContext : DbContext
{
    public DbSet<Users> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=myUsers;Username=;Password=");
    }
}