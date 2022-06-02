namespace WebApi.Helpers;

using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

public class DataContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public DataContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to sql server database
        //options.UseSqlServer(Configuration.GetConnectionString("WebApiDatabase"));
        options.UseSqlServer(Configuration.GetConnectionString("WebApiDatabase"));
        //"Data Source=LAPTOP-LANAVNCO\\SQLEXPRESS;Initial Catalog=WebApiDB;Integrated Security=true;"
    }
    protected override void OnModelCreating(ModelBuilder modelbuilder)
    {
        modelbuilder.Entity<User>().ToTable("Users");
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
}