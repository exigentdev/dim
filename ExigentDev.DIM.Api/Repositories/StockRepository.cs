using ExigentDev.DIM.Api.Data;
using ExigentDev.DIM.Api.Dtos.Stock;
using ExigentDev.DIM.Api.Interfaces;
using ExigentDev.DIM.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ExigentDev.DIM.Api.Repositories
{
  public class StockRepository(ApplicationDBContext context) : IStockRepository
  {
    private readonly ApplicationDBContext _context = context;

    public async Task<Stock> CreateAsync(Stock stockModel)
    {
      await _context.Stocks.AddAsync(stockModel);
      await _context.SaveChangesAsync();

      return stockModel;
    }

    public async Task<Stock?> DeleteAsync(int id)
    {
      var stockModel = await _context.Stocks.FindAsync(id);
      if (stockModel == null)
      {
        return null;
      }

      _context.Stocks.Remove(stockModel);
      await _context.SaveChangesAsync();

      return stockModel;
    }

    public async Task<List<Stock>> GetAllAsync()
    {
      return await _context.Stocks.Include(c => c.Comments).ToListAsync();
    }

    public async Task<Stock?> GetByIdAsync(int id)
    {
      return await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto)
    {
      var existingStock = await _context.Stocks.FindAsync(id);
      if (existingStock == null)
      {
        return null;
      }

      _context.Entry(existingStock).CurrentValues.SetValues(stockDto);
      await _context.SaveChangesAsync();

      return existingStock;
    }
  }
}
