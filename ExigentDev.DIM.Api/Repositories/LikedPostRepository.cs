using ExigentDev.DIM.Api.Data;
using ExigentDev.DIM.Api.Interfaces;
using ExigentDev.DIM.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ExigentDev.DIM.Api.Repositories
{
  public class LikedPostRepository(ApplicationDBContext context) : ILikedPostRepository
  {
    private readonly ApplicationDBContext _context = context;

    public async Task<LikedPost> LikePostAsync(LikedPost likedPostModel)
    {
      await _context.LikedPosts.AddAsync(likedPostModel);
      await _context.SaveChangesAsync();

      return likedPostModel;
    }

    public async Task<LikedPost?> FindLikedPostAsync(LikedPost likedPostModel)
    {
      return await _context.LikedPosts.FirstOrDefaultAsync(
        (likedPost) =>
          likedPost.PostId == likedPostModel.PostId
          && likedPost.AppUserId == likedPostModel.AppUserId
      );
    }

    public async Task<LikedPost> UnlikePostAsync(LikedPost likedPostModel)
    {
      _context.LikedPosts.Remove(likedPostModel);
      await _context.SaveChangesAsync();

      return likedPostModel;
    }
  }
}
