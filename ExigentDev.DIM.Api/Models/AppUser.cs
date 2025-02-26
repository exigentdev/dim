using Microsoft.AspNetCore.Identity;

namespace ExigentDev.DIM.Api.Models
{
  public class AppUser : IdentityUser
  {
    public List<Portfolio> Portfolios { get; set; } = [];
  }
}
