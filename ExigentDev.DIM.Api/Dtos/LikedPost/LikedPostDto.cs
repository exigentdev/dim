namespace ExigentDev.DIM.Api.Dtos.LikedPost
{
  public class LikedPostDto
  {
    public int Id { get; set; }
    public string AppUserId { get; set; } = string.Empty;
    public string ProfileImageUrl { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public int PostId { get; set; }
  }
}
