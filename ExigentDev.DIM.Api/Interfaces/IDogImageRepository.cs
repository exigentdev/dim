using ExigentDev.DIM.Api.Models;

namespace ExigentDev.DIM.Api.Interfaces
{
  public interface IDogImageRepository
  {
    public Task CreateManyAsync(List<DogImage> dogImages);
  }
}
