using ExigentDev.DIM.Api.Data;
using ExigentDev.DIM.Api.Helpers;
using ExigentDev.DIM.Api.Interfaces;
using ExigentDev.DIM.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ExigentDev.DIM.Api.Repositories
{
  public class PostRepository(ApplicationDBContext context) : IPostRepository
  {
    private readonly ApplicationDBContext _context = context;

    public async Task<Post> CreateAsync(Post postModel)
    {
      await _context.Posts.AddAsync(postModel);
      await _context.SaveChangesAsync();

      return postModel;
    }

    public async Task<List<Post>> GetAllAsync(QueryObject queryObject)
    {
      var posts = _context.Posts.AsQueryable();

      var skipNumber = (queryObject.PageNumber - 1) * queryObject.PageSize;

      return await posts.Skip(skipNumber).Take(queryObject.PageSize).ToListAsync();
    }

    public async Task<Post?> GetByIdAsync(int id)
    {
      return await _context
        .Posts.Include(post => post.AppUser)
        .Include(post => post.Dog)
        .ThenInclude(dog => dog.DogImages)
        .Include(post => post.LikedPosts)
        .ThenInclude(likedPost => likedPost.AppUser)
        .FirstOrDefaultAsync(post => post.Id == id);
    }
  }
}
