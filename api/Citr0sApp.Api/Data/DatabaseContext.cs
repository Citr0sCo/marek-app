using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Citr0sApp.Api.Data;

public class DatabaseContext : DbContext
{
    public DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        Directory.CreateDirectory("assets");
        optionsBuilder.UseSqlite("Data Source=assets/marek-app.db");

        // The schema is managed outside EF, so no migrations exist; EF 11 throws on Migrate() by default.
        optionsBuilder.ConfigureWarnings(w => w.Ignore(RelationalEventId.MigrationsNotFound));

        var value = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        if (value == "Development")
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}