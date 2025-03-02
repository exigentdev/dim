using ExigentDev.DIM.Api.Extensions;
using ExigentDev.DIM.Api.Interfaces;
using ExigentDev.DIM.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExigentDev.DIM.Api.Controllers
{
  [Route("api/portfolio")]
  [ApiController]
  public class PortfolioController(
    UserManager<AppUser> userManager,
    IStockRepository stockRepository,
    IPortfolioRepository portfolioRepository
  ) : ControllerBase
  {
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly IStockRepository _stockRepository = stockRepository;
    private readonly IPortfolioRepository _portfolioRepository = portfolioRepository;

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUserPortfolio()
    {
      var username = User.GetUsername();
      var appUser = await _userManager.FindByNameAsync(username);

      var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser!);

      return Ok(userPortfolio);
    }
  }
}
