using ExigentDev.DIM.Api.Dtos.Account;
using ExigentDev.DIM.Api.Interfaces;
using ExigentDev.DIM.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExigentDev.DIM.Api.Controllers
{
  [Route("api/account")]
  [ApiController]
  public class AccountController(UserManager<AppUser> userManager, ITokenService tokenService)
    : ControllerBase
  {
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly ITokenService _tokenService = tokenService;

    [HttpPost("register")]
    public async Task<IActionResult> register([FromBody] RegisterDto registerDto)
    {
      try
      {
        if (!ModelState.IsValid)
        {
          return BadRequest();
        }

        var appUser = new AppUser { UserName = registerDto.Username, Email = registerDto.Email };

        var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password!);
        if (createdUser.Succeeded)
        {
          var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
          if (roleResult.Succeeded)
          {
            return Ok(
              new NewUserDto
              {
                UserName = appUser.UserName!,
                Email = appUser.Email!,
                Token = _tokenService.CreateToken(appUser),
              }
            );
          }
          else
          {
            return BadRequest(roleResult.Errors);
          }
        }
        else
        {
          return BadRequest(createdUser.Errors);
        }
      }
      catch (Exception e)
      {
        return BadRequest(e);
      }
    }
  }
}
