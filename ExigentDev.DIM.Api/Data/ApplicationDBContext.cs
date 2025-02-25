using ExigentDev.DIM.Api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExigentDev.DIM.Api.Data
{
  public class ApplicationDBContext(DbContextOptions dbContextOptions)
    : IdentityDbContext<AppUser>(dbContextOptions)
  {
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<Comment> Comments { get; set; }
  }
}
