using ExigentDev.DIM.Api.Interfaces;
using ExigentDev.DIM.Api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace ExigentDev.DIM.Api.Controllers
{
  [Route("api/comment")]
  [ApiController]
  public class CommentController(ICommentRepository commentRepository) : ControllerBase
  {
    private readonly ICommentRepository _commentRepo = commentRepository;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var comments = await _commentRepo.GetAllAsync();

      var commentDto = comments.Select(c => c.ToCommentDto());

      return Ok(commentDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
      var comment = await _commentRepo.GetByIdAsync(id);

      if (comment == null)
      {
        return NotFound();
      }

      return Ok(comment.ToCommentDto());
    }
  }
}
