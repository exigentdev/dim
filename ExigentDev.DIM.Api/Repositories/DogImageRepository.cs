using ExigentDev.DIM.Api.Data;
using ExigentDev.DIM.Api.Interfaces;
using ExigentDev.DIM.Api.Models;

namespace ExigentDev.DIM.Api.Repositories
{
  public class DogImageRepository(ApplicationDBContext context) : IDogImageRepository
  {
    private readonly ApplicationDBContext _context = context;

    public async Task CreateManyAsync(List<DogImage> dogImages)
    {
      await _context.DogImages.AddRangeAsync(dogImages);
      await _context.SaveChangesAsync();
    }
  }
}
