using Microsoft.EntityFrameworkCore;
using TaskAPI.Entities;
namespace TaskAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
    public DbSet<TaskEntitie> Tasks { get; set; }
    public DbSet<CategoryEntitie> Categories { get; set; }
    public DbSet<UserEntitie> Users { get; set; }

}
