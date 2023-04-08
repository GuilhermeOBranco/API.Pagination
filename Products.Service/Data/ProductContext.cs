using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using Products.Domain.Models;

namespace Products.Service.Data;

public class ProductContext : DbContext
{
    private readonly IConfiguration _configuration;

    public ProductContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public DbSet<Product> Product { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine);
        base.OnConfiguring(optionsBuilder.UseMySql(
            serverVersion: ServerVersion.AutoDetect(_configuration.GetConnectionString(("SqlAWS"))), connectionString: _configuration.GetConnectionString("SqlAWS")));
    }
}