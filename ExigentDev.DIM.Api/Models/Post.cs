using System.ComponentModel.DataAnnotations.Schema;

namespace ExigentDev.DIM.Api.Models
{
  [Table("Post")]
  public class Post
  {
    public int Id { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    public string AppUserId { get; set; } = string.Empty;
    public AppUser AppUser { get; set; } = null!;
    public int DogId { get; set; }
    public Dog Dog { get; set; } = null!;
    public List<LikedPost> LikedPosts { get; set; } = [];
  }
}
