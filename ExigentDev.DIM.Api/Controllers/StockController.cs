using ExigentDev.DIM.Api.Data;
using ExigentDev.DIM.Api.Dtos.Stock;
using ExigentDev.DIM.Api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExigentDev.DIM.Api.Controllers
{
  [Route("api/stock")]
  [ApiController]
  public class StockController(ApplicationDBContext context) : ControllerBase
  {
    private readonly ApplicationDBContext _context = context;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var stocks = await _context.Stocks.ToListAsync();

      var stockDto = stocks.Select(s => s.ToStockDto());

      return Ok(stockDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
      var stock = await _context.Stocks.FindAsync(id);

      if (stock == null)
      {
        return NotFound();
      }

      return Ok(stock.ToStockDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
    {
      var stockModel = stockDto.ToStockFromCreateDTO();

      await _context.Stocks.AddAsync(stockModel);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
      [FromRoute] int id,
      [FromBody] UpdateStockRequestDto updateDto
    )
    {
      var stockModel = await _context.Stocks.FindAsync(id);

      if (stockModel == null)
      {
        return NotFound();
      }

      _context.Entry(stockModel).CurrentValues.SetValues(updateDto);
      await _context.SaveChangesAsync();

      return Ok(stockModel.ToStockDto());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
      var stockModel = await _context.Stocks.FindAsync(id);

      if (stockModel == null)
      {
        return NotFound();
      }

      _context.Stocks.Remove(stockModel);
      await _context.SaveChangesAsync();

      return NoContent();
    }
  }
}
