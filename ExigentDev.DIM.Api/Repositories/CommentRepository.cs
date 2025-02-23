using ExigentDev.DIM.Api.Data;
using ExigentDev.DIM.Api.Dtos.Comment;
using ExigentDev.DIM.Api.Interfaces;
using ExigentDev.DIM.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExigentDev.DIM.Api.Repositories
{
  public class CommentRepository(ApplicationDBContext context) : ICommentRepository
  {
    private ApplicationDBContext _context = context;

    public async Task<Comment> CreateAsync(Comment commentModel)
    {
      await _context.Comments.AddAsync(commentModel);
      await _context.SaveChangesAsync();

      return commentModel;
    }

    public async Task<Comment?> DeleteAsync(int id)
    {
      var commentModel = await _context.Comments.FindAsync(id);

      if (commentModel == null)
      {
        return null;
      }

      _context.Comments.Remove(commentModel);
      await _context.SaveChangesAsync();

      return commentModel;
    }

    public async Task<List<Comment>> GetAllAsync()
    {
      return await _context.Comments.ToListAsync();
    }

    public async Task<Comment?> GetByIdAsync([FromRoute] int id)
    {
      return await _context.Comments.FindAsync(id);
    }

    public async Task<Comment?> UpdateAsync(int id, UpdateCommentDto commentDto)
    {
      var existingComment = await _context.Comments.FindAsync(id);

      if (existingComment == null)
      {
        return null;
      }

      _context.Entry(existingComment).CurrentValues.SetValues(commentDto);
      await _context.SaveChangesAsync();

      return existingComment;
    }
  }
}
