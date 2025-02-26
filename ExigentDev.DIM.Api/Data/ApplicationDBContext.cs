using ExigentDev.DIM.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExigentDev.DIM.Api.Data
{
  public class ApplicationDBContext(DbContextOptions dbContextOptions)
    : IdentityDbContext<AppUser>(dbContextOptions)
  {
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Portfolio> Portfolios { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);

      builder.Entity<Portfolio>(x =>
        x.HasKey(portfolio => new { portfolio.AppUserId, portfolio.StockId })
      );

      builder
        .Entity<Portfolio>()
        .HasOne(portfolio => portfolio.AppUser)
        .WithMany(user => user.Portfolios)
        .HasForeignKey(portfolio => portfolio.AppUserId);

      builder
        .Entity<Portfolio>()
        .HasOne(portfolio => portfolio.Stock)
        .WithMany(stock => stock.Portfolios)
        .HasForeignKey(portfolio => portfolio.StockId);

      List<IdentityRole> roles =
      [
        new IdentityRole
        {
          Id = "Admin",
          Name = "Admin",
          NormalizedName = "ADMIN",
        },
        new IdentityRole
        {
          Id = "User",
          Name = "User",
          NormalizedName = "USER",
        },
      ];

      builder.Entity<IdentityRole>().HasData(roles);
    }
  }
}
