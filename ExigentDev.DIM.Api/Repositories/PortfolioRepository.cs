using ExigentDev.DIM.Api.Data;
using ExigentDev.DIM.Api.Interfaces;
using ExigentDev.DIM.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ExigentDev.DIM.Api.Repositories
{
  public class PortfolioRepository(ApplicationDBContext context) : IPortfolioRepository
  {
    private readonly ApplicationDBContext _context = context;

    public async Task<List<Stock>> GetUserPortfolio(AppUser user)
    {
      return await _context
        .Portfolios.Where(u => u.AppUserId == user.Id)
        .Select(stock => new Stock
        {
          Id = stock.StockId,
          Symbol = stock.Stock.Symbol,
          CompanyName = stock.Stock.CompanyName,
          Purchase = stock.Stock.Purchase,
          LastDiv = stock.Stock.LastDiv,
          Industry = stock.Stock.Industry,
          MarketCap = stock.Stock.MarketCap,
        })
        .ToListAsync();
    }
  }
}
