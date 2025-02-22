using ExigentDev.DIM.Api.Data;
using ExigentDev.DIM.Api.Interfaces;
using ExigentDev.DIM.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExigentDev.DIM.Api.Repositories
{
  public class CommentRepository(ApplicationDBContext context) : ICommentRepository
  {
    private ApplicationDBContext _context = context;

    public async Task<List<Comment>> GetAllAsync()
    {
      return await _context.Comments.ToListAsync();
    }

    public async Task<Comment?> GetByIdAsync([FromRoute] int id)
    {
      return await _context.Comments.FindAsync(id);
    }
  }
}
