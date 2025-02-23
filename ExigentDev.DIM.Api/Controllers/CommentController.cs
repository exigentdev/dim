using ExigentDev.DIM.Api.Dtos.Comment;
using ExigentDev.DIM.Api.Interfaces;
using ExigentDev.DIM.Api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace ExigentDev.DIM.Api.Controllers
{
  [Route("api/comment")]
  [ApiController]
  public class CommentController(
    ICommentRepository commentRepository,
    IStockRepository stockRepository
  ) : ControllerBase
  {
    private readonly ICommentRepository _commentRepo = commentRepository;
    private readonly IStockRepository _stockRepo = stockRepository;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var comments = await _commentRepo.GetAllAsync();

      var commentDto = comments.Select(c => c.ToCommentDto());

      return Ok(commentDto);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      var comment = await _commentRepo.GetByIdAsync(id);

      if (comment == null)
      {
        return NotFound("Comment not found");
      }

      return Ok(comment.ToCommentDto());
    }

    [HttpPost("{stockId:int}")]
    public async Task<IActionResult> Create(
      [FromRoute] int stockId,
      [FromBody] CreateCommentDto createCommentDto
    )
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      if (!await _stockRepo.StockExists(stockId))
      {
        return BadRequest("Stock does not exist");
      }

      var commentModel = createCommentDto.ToCommentFromCreateDto(stockId);
      await _commentRepo.CreateAsync(commentModel);

      return CreatedAtAction(
        nameof(GetById),
        new { id = commentModel.Id },
        commentModel.ToCommentDto()
      );
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
      [FromRoute] int id,
      [FromBody] UpdateCommentDto updateCommentDto
    )
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      var commentModel = await _commentRepo.UpdateAsync(id, updateCommentDto);
      if (commentModel == null)
      {
        return NotFound("Comment not found");
      }

      return Ok(commentModel.ToCommentDto());
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      var commentModel = await _commentRepo.DeleteAsync(id);

      if (commentModel == null)
      {
        return NotFound("Comment not found");
      }

      return NoContent();
    }
  }
}
