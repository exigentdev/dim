using ExigentDev.DIM.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ExigentDev.DIM.Api.Data
{
  public class ApplicationDBContext(DbContextOptions dbContextOptions) : DbContext(dbContextOptions)
  {
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<Comment> Comments { get; set; }
  }
}
