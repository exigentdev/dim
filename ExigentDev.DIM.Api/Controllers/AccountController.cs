using ExigentDev.DIM.Api.Dtos.Account;
using ExigentDev.DIM.Api.Interfaces;
using ExigentDev.DIM.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExigentDev.DIM.Api.Controllers
{
  [Route("api/account")]
  [ApiController]
  public class AccountController(
    UserManager<AppUser> userManager,
    SignInManager<AppUser> signInManager,
    ITokenService tokenService
  ) : ControllerBase
  {
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly SignInManager<AppUser> _signInManager = signInManager;
    private readonly ITokenService _tokenService = tokenService;

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
      try
      {
        if (!ModelState.IsValid)
        {
          return BadRequest();
        }

        var appUser = new AppUser { UserName = registerDto.Username, Email = registerDto.Email };

        var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password!);

        if (!createdUser.Succeeded)
        {
          return BadRequest(createdUser.Errors);
        }

        var roleResult = await _userManager.AddToRoleAsync(appUser, "User");

        if (!roleResult.Succeeded)
        {
          return BadRequest(roleResult.Errors);
        }

        return Ok(
          new NewUserDto
          {
            UserName = appUser.UserName!,
            Email = appUser.Email!,
            Token = _tokenService.CreateToken(appUser),
          }
        );
      }
      catch (Exception e)
      {
        return BadRequest(e);
      }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var user = await _userManager.Users.FirstOrDefaultAsync(user =>
        user.UserName == loginDto.Username
      );

      if (user == null)
      {
        return Unauthorized("Invalid username");
      }

      var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

      if (!result.Succeeded)
      {
        return Unauthorized("Username not found and/or password incorrect");
      }

      return Ok(
        new NewUserDto
        {
          UserName = user.UserName!,
          Email = user.Email!,
          Token = _tokenService.CreateToken(user),
        }
      );
    }
  }
}
