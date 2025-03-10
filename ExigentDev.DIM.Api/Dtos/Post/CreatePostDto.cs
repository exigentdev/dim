namespace ExigentDev.DIM.Api.Dtos.Post
{
  public class CreatePostDto
  {
    public List<string> DogImageUrls { get; set; } = [];
    public DateTime DateMet { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public string Breed { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
  }
}
