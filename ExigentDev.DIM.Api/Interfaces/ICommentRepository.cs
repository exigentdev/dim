using ExigentDev.DIM.Api.Models;

namespace ExigentDev.DIM.Api.Interfaces
{
  public interface ICommentRepository
  {
    Task<List<Comment>> GetAllAsync();
    Task<Comment?> GetByIdAsync(int id);
  }
}
