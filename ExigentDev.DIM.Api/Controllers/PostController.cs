using System.Security.Claims;
using ExigentDev.DIM.Api.Dtos.Post;
using ExigentDev.DIM.Api.Helpers;
using ExigentDev.DIM.Api.Interfaces;
using ExigentDev.DIM.Api.Mappers;
using ExigentDev.DIM.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExigentDev.DIM.Api.Controllers
{
  [Route("api/post")]
  [ApiController]
  public class PostController(
    UserManager<AppUser> userManager,
    IPostRepository postRepository,
    IDogRepository dogRepository,
    IDogImageRepository dogImageRepository
  ) : ControllerBase
  {
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly IPostRepository _postRepository = postRepository;
    private readonly IDogRepository _dogRepository = dogRepository;
    private readonly IDogImageRepository _dogImageRepository = dogImageRepository;

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] QueryObject queryObject)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var posts = await _postRepository.GetAllAsync(queryObject);

      var postsDtos = posts.Select(post => post.ToPostDto());

      return Ok(postsDtos);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var postModel = await _postRepository.GetByIdAsync(id);

      if (postModel == null)
      {
        return NotFound("Post not found");
      }

      return Ok(postModel.ToPostDto());
    }

    [Authorize]
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreatePostDto createPostDto)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var username = User.Identity?.Name;
      if (username == null)
      {
        return BadRequest("UserName not found");
      }

      var appUser = await _userManager.FindByNameAsync(username);
      if (appUser == null)
      {
        return BadRequest("User not found");
      }

      var appUserId = appUser.Id;

      var dogModel = createPostDto.ToDogFromCreatePostDto(appUserId);
      var savedDog = await _dogRepository.CreateAsync(dogModel);

      var dogImages = createPostDto.ToDogImageListFromCreatePostDto(savedDog.Id);

      await _dogImageRepository.CreateManyAsync(dogImages);

      var postModel = new Post { AppUserId = appUserId, DogId = savedDog.Id };

      await _postRepository.CreateAsync(postModel);

      return CreatedAtAction(nameof(GetById), new { id = postModel.Id }, new { id = postModel.Id });
    }
  }
}
