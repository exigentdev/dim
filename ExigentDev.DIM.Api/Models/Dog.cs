using System.ComponentModel.DataAnnotations.Schema;

namespace ExigentDev.DIM.Api.Models
{
  [Table("Dog")]
  public class Dog
  {
    public int Id { get; set; }
    public string AppUserId { get; set; } = string.Empty;
    public AppUser AppUser { get; set; } = null!;
    public List<DogImage> DogImages { get; set; } = [];
    public DateTime DateMet { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public string Breed { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
  }
}
