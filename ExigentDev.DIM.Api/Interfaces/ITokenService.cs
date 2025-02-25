using ExigentDev.DIM.Api.Models;

namespace ExigentDev.DIM.Api.Interfaces
{
  public interface ITokenService
  {
    string CreateToken(AppUser appUser);
  }
}
