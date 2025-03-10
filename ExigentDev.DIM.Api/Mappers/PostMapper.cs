using ExigentDev.DIM.Api.Dtos.Post;
using ExigentDev.DIM.Api.Models;

namespace ExigentDev.DIM.Api.Mappers
{
  public static class PostMapper
  {
    public static PostDto ToPostDto(this Post postModel)
    {
      return new PostDto
      {
        Id = postModel.Id,
        DateCreated = postModel.DateCreated,
        AppUser = postModel.AppUser.ToAppUserDto(),
        Dog = postModel.Dog.ToDogDto(),
        LikedByUsers =
        [
          .. postModel.LikedPosts.Select(likedPost => likedPost.AppUser.ToAppUserDto()),
        ],
      };
    }

    public static List<DogImage> ToDogImageListFromCreatePostDto(
      this CreatePostDto createPostDto,
      int dogId
    )
    {
      return
      [
        .. createPostDto.DogImageUrls.Select(url => new DogImage { ImageUrl = url, DogId = dogId }),
      ];
    }

    public static Dog ToDogFromCreatePostDto(this CreatePostDto createPostDto, string appUserId)
    {
      return new Dog
      {
        AppUserId = appUserId,
        DateMet = createPostDto.DateMet,
        Rating = createPostDto.Rating,
        Comment = createPostDto.Comment,
        Breed = createPostDto.Breed,
        Name = createPostDto.Name,
      };
    }
  }
}
