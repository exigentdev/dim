using ExigentDev.DIM.Api.Models;

namespace ExigentDev.DIM.Api.Dtos.Dog
{
  public class DogDto
  {
    public int Id { get; set; }
    public DateTime DateMet { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public string Breed { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public List<DogImage> DogImages { get; set; } = [];
  }
}
