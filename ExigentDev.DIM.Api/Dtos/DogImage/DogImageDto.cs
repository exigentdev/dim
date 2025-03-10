namespace ExigentDev.DIM.Api.Dtos.DogImage
{
  public class DogImageDto
  {
    public int Id { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public int DogId { get; set; }
  }
}
