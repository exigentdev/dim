// using System.Security.Claims;
// using ExigentDev.DIM.Api.Interfaces;
// using ExigentDev.DIM.Api.Models;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Mvc;

// namespace ExigentDev.DIM.Api.Controllers
// {
//   [Route("api/portfolio")]
//   [ApiController]
//   public class PortfolioController(
//     UserManager<AppUser> userManager,
//     IStockRepository stockRepository,
//     IPortfolioRepository portfolioRepository,
//     IHttpContextAccessor httpContextAccessor
//   ) : ControllerBase
//   {
//     private readonly UserManager<AppUser> _userManager = userManager;
//     private readonly IStockRepository _stockRepository = stockRepository;
//     private readonly IPortfolioRepository _portfolioRepository = portfolioRepository;
//     private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

//     [HttpGet]
//     [Authorize]
//     public async Task<IActionResult> GetUserPortfolio()
//     {
//       var username = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.GivenName);
//       var appUser = await _userManager.FindByNameAsync(username!);

//       var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser!);

//       return Ok(userPortfolio);
//     }

//     [HttpPost]
//     [Authorize]
//     public async Task<IActionResult> AddPortfolio(string symbol)
//     {
//       var username = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.GivenName);
//       var appUser = await _userManager.FindByNameAsync(username!);

//       var stock = await _stockRepository.GetBySymbolAsync(symbol);

//       if (stock == null)
//       {
//         return BadRequest("Stock not found");
//       }

//       var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser!);

//       if (userPortfolio.Any(e => e.Symbol.ToLower() == symbol.ToLower()))
//       {
//         return BadRequest("Cannot add stock to portfolio");
//       }

//       var portfolioModel = new Portfolio { StockId = stock.Id, AppUserId = appUser!.Id };

//       await _portfolioRepository.CreateAsync(portfolioModel);

//       if (portfolioModel == null)
//       {
//         return StatusCode(500, "Could not create portfolio");
//       }
//       else
//       {
//         return Created();
//       }
//     }
//   }
// }
