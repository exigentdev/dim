using System.ComponentModel.DataAnnotations.Schema;

namespace ExigentDev.DIM.Api.Models
{
  [Table("LikedPost")]
  public class LikedPost
  {
    public int Id { get; set; }
    public string AppUserId { get; set; } = string.Empty;
    public AppUser AppUser { get; set; } = null!;
    public int PostId { get; set; }
    public Post Post { get; set; } = null!;
  }
}
