namespace ExigentDev.DIM.Api.Models
{
  public class DogImage
  {
    public int Id { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public int DogId { get; set; }
    public Dog Dog { get; set; } = null!;
  }
}
