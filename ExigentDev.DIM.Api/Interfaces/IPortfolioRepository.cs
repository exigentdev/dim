using ExigentDev.DIM.Api.Models;

namespace ExigentDev.DIM.Api.Interfaces
{
  public interface IPortfolioRepository
  {
    Task<List<Stock>> GetUserPortfolio(AppUser user);
  }
}
