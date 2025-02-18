using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExigentDev.DIM.Api.Data;
using ExigentDev.DIM.Api.Dtos.Stock;
using ExigentDev.DIM.Api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace ExigentDev.DIM.Api.Controllers
{
  [Route("api/stock")]
  [ApiController]
  public class StockController(ApplicationDBContext context) : ControllerBase
  {
    private readonly ApplicationDBContext _context = context;

    [HttpGet]
    public IActionResult GetAll()
    {
      var stocks = _context.Stocks.ToList().Select(s => s.ToStockDto());

      return Ok(stocks);
    }

    [HttpGet("{id}")]
    public IActionResult GetById([FromRoute] int id)
    {
      var stock = _context.Stocks.Find(id);

      if (stock == null)
      {
        return NotFound();
      }

      return Ok(stock.ToStockDto());
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateStockRequestDto stockDto)
    {
      var stockModel = stockDto.ToStockFromCreateDTO();

      _context.Stocks.Add(stockModel);
      _context.SaveChanges();

      return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
    }

    [HttpPut("{id}")]
    public IActionResult Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
    {
      var stockModel = _context.Stocks.Find(id);

      if (stockModel == null)
      {
        return NotFound();
      }

      _context.Entry(stockModel).CurrentValues.SetValues(updateDto);
      _context.SaveChanges();

      return Ok(stockModel.ToStockDto());
    }

    [HttpDelete("{id}")]
    public IActionResult Delete([FromRoute] int id)
    {
      var stockModel = _context.Stocks.Find(id);

      if (stockModel == null)
      {
        return NotFound();
      }

      _context.Stocks.Remove(stockModel);
      _context.SaveChanges();

      return NoContent();
    }
  }
}
