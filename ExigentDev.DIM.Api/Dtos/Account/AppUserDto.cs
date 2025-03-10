namespace ExigentDev.DIM.Api.Dtos.Account
{
  public class AppUserDto
  {
    public string UserName { get; set; } = string.Empty;
    public DateTime DateJoined { get; set; } = DateTime.UtcNow;
    public string ProfileImageUrl { get; set; } = string.Empty;
  }
}
