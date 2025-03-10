using ExigentDev.DIM.Api.Models;

namespace ExigentDev.DIM.Api.Interfaces
{
  public interface IDogRepository
  {
    Task<Dog> CreateAsync(Dog dogModel);
  }
}
