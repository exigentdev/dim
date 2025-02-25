using ExigentDev.DIM.Api.Dtos.Account;
using ExigentDev.DIM.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExigentDev.DIM.Api.Controllers
{
  [Route("api/account")]
  [ApiController]
  public class AccountController(UserManager<AppUser> userManager) : ControllerBase
  {
    private readonly UserManager<AppUser> _userManager = userManager;

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
            return Ok("User Created");
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
