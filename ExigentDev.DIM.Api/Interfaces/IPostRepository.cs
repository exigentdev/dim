using ExigentDev.DIM.Api.Helpers;
using ExigentDev.DIM.Api.Models;

namespace ExigentDev.DIM.Api.Interfaces
{
  public interface IPostRepository
  {
    Task<List<Post>> GetAllAsync(QueryObject queryObject);
    Task<Post?> GetByIdAsync(int id);
    Task<Post> CreateAsync(Post postModel);
  }
}
