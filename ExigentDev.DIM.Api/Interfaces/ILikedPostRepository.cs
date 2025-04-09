using ExigentDev.DIM.Api.Models;

namespace ExigentDev.DIM.Api.Interfaces
{
  public interface ILikedPostRepository
  {
    Task<LikedPost> LikePostAsync(LikedPost likedPostModel);
    Task<LikedPost> UnlikePostAsync(LikedPost likedPostModel);
    Task<LikedPost?> FindLikedPostAsync(LikedPost likedPostModel);
  }
}
