// using ExigentDev.DIM.Api.Dtos.Stock;
// using ExigentDev.DIM.Api.Helpers;
// using ExigentDev.DIM.Api.Interfaces;
// using ExigentDev.DIM.Api.Mappers;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;

// namespace ExigentDev.DIM.Api.Controllers
// {
//   [Route("api/stock")]
//   [ApiController]
//   public class StockController(IStockRepository stockRepo) : ControllerBase
//   {
//     private readonly IStockRepository _stockRepo = stockRepo;

//     [Authorize]
//     [HttpGet]
//     public async Task<IActionResult> GetAll([FromQuery] QueryObject queryObject)
//     {
//       if (!ModelState.IsValid)
//       {
//         return BadRequest(ModelState);
//       }
//       var stocks = await _stockRepo.GetAllAsync(queryObject);

//       var stockDto = stocks.Select(s => s.ToStockDto());

//       return Ok(stockDto);
//     }

//     [HttpGet("{id:int}")]
//     public async Task<IActionResult> GetById([FromRoute] int id)
//     {
//       if (!ModelState.IsValid)
//       {
//         return BadRequest(ModelState);
//       }
//       var stock = await _stockRepo.GetByIdAsync(id);

//       if (stock == null)
//       {
//         return NotFound("Stock not found");
//       }

//       return Ok(stock.ToStockDto());
//     }

//     [HttpPost]
//     public async Task<IActionResult> Create([FromBody] CreateStockDto stockDto)
//     {
//       if (!ModelState.IsValid)
//       {
//         return BadRequest(ModelState);
//       }
//       var stockModel = stockDto.ToStockFromCreateDto();

//       await _stockRepo.CreateAsync(stockModel);

//       return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
//     }

//     [HttpPut("{id:int}")]
//     public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockDto updateDto)
//     {
//       if (!ModelState.IsValid)
//       {
//         return BadRequest(ModelState);
//       }
//       var stockModel = await _stockRepo.UpdateAsync(id, updateDto);

//       if (stockModel == null)
//       {
//         return NotFound("Stock not found");
//       }

//       return Ok(stockModel.ToStockDto());
//     }

//     [HttpDelete("{id:int}")]
//     public async Task<IActionResult> Delete([FromRoute] int id)
//     {
//       if (!ModelState.IsValid)
//       {
//         return BadRequest(ModelState);
//       }
//       var stockModel = await _stockRepo.DeleteAsync(id);

//       if (stockModel == null)
//       {
//         return NotFound("Stock not found");
//       }

//       return NoContent();
//     }
//   }
// }
