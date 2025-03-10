using Microsoft.AspNetCore.Identity;

namespace ExigentDev.DIM.Api.Models
{
  public class AppUser : IdentityUser
  {
    public List<Post> Posts { get; set; } = [];
    public List<Dog> Dogs { get; set; } = [];
    public List<LikedPost> LikedPosts { get; set; } = [];
    public DateTime DateJoined { get; set; } = DateTime.UtcNow;
    public string ProfileImageUrl { get; set; } = string.Empty;
  }
}
