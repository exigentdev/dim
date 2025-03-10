using ExigentDev.DIM.Api.Dtos.Account;
using ExigentDev.DIM.Api.Dtos.Dog;
using ExigentDev.DIM.Api.Dtos.LikedPost;

namespace ExigentDev.DIM.Api.Dtos.Post
{
  public class PostDto
  {
    public int Id { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    public AppUserDto AppUser { get; set; } = null!;
    public DogDto Dog { get; set; } = null!;
    public List<LikedPostDto> LikedByUsers { get; set; } = [];
  }
}
