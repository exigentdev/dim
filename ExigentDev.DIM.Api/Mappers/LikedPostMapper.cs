using ExigentDev.DIM.Api.Dtos.LikedPost;
using ExigentDev.DIM.Api.Models;

namespace ExigentDev.DIM.Api.Mappers
{
  public static class LikedPostMapper
  {
    public static LikedPostDto ToLikedPostDto(this LikedPost likedPostModel)
    {
      return new LikedPostDto
      {
        Id = likedPostModel.Id,
        AppUserId = likedPostModel.AppUserId,
        ProfileImageUrl = likedPostModel.AppUser.ProfileImageUrl,
        UserName = likedPostModel.AppUser.UserName!,
        PostId = likedPostModel.PostId,
      };
    }
  }
}
