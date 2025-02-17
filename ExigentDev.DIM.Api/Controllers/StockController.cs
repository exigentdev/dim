using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExigentDev.DIM.Api.Data;
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
      var stocks = _context.Stocks.ToList();

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

      return Ok(stock);
    }
  }
}
