using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using SirketData;
using System.IO;

public class SirketDbContextFactory : IDesignTimeDbContextFactory<SirketDbContext>
{
    public SirketDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
    .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..\\WebApplication3"))  // appsettings.json'un olduğu dizin
    .AddJsonFile("appsettings.json")
    .Build();


        var optionsBuilder = new DbContextOptionsBuilder<SirketDbContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        optionsBuilder.UseSqlServer(connectionString);

        return new SirketDbContext(optionsBuilder.Options);
    }
}
