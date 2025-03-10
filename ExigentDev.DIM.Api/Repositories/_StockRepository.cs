// using ExigentDev.DIM.Api.Data;
// using ExigentDev.DIM.Api.Dtos.Stock;
// using ExigentDev.DIM.Api.Helpers;
// using ExigentDev.DIM.Api.Interfaces;
// using ExigentDev.DIM.Api.Models;
// using Microsoft.EntityFrameworkCore;

// namespace ExigentDev.DIM.Api.Repositories
// {
//   public class StockRepository(ApplicationDBContext context) : IStockRepository
//   {
//     private readonly ApplicationDBContext _context = context;

//     public async Task<Stock> CreateAsync(Stock stockModel)
//     {
//       await _context.Stocks.AddAsync(stockModel);
//       await _context.SaveChangesAsync();

//       return stockModel;
//     }

//     public async Task<Stock?> DeleteAsync(int id)
//     {
//       var stockModel = await _context.Stocks.FindAsync(id);
//       if (stockModel == null)
//       {
//         return null;
//       }

//       _context.Stocks.Remove(stockModel);
//       await _context.SaveChangesAsync();

//       return stockModel;
//     }

//     public async Task<List<Stock>> GetAllAsync(QueryObject queryObject)
//     {
//       var stocks = _context.Stocks.Include(c => c.Comments).AsQueryable();

//       if (!string.IsNullOrWhiteSpace(queryObject.CompanyName))
//       {
//         stocks = stocks.Where(s => s.CompanyName.Contains(queryObject.CompanyName));
//       }

//       if (!string.IsNullOrWhiteSpace(queryObject.Symbol))
//       {
//         stocks = stocks.Where(s => s.Symbol.Contains(queryObject.Symbol));
//       }
//       if (!string.IsNullOrWhiteSpace(queryObject.SortBy))
//       {
//         if (queryObject.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
//         {
//           stocks = queryObject.IsDescending
//             ? stocks.OrderByDescending(s => s.Symbol)
//             : stocks.OrderBy(s => s.Symbol);
//         }
//       }

//       var skipNumber = (queryObject.PageNumber - 1) * queryObject.PageSize;

//       return await stocks.Skip(skipNumber).Take(queryObject.PageSize).ToListAsync();
//     }

//     public async Task<Stock?> GetByIdAsync(int id)
//     {
//       return await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(s => s.Id == id);
//     }

//     public async Task<Stock?> UpdateAsync(int id, UpdateStockDto stockDto)
//     {
//       var existingStock = await _context.Stocks.FindAsync(id);
//       if (existingStock == null)
//       {
//         return null;
//       }

//       _context.Entry(existingStock).CurrentValues.SetValues(stockDto);
//       await _context.SaveChangesAsync();

//       return existingStock;
//     }

//     public Task<bool> StockExists(int id)
//     {
//       return _context.Stocks.AnyAsync(s => s.Id == id);
//     }

//     public async Task<Stock?> GetBySymbolAsync(string symbol)
//     {
//       return await _context.Stocks.FirstOrDefaultAsync(s => s.Symbol == symbol);
//     }
//   }
// }
