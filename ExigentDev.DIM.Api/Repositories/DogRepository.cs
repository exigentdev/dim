using ExigentDev.DIM.Api.Data;
using ExigentDev.DIM.Api.Interfaces;
using ExigentDev.DIM.Api.Models;

namespace ExigentDev.DIM.Api.Repositories
{
  public class DogRepository(ApplicationDBContext context) : IDogRepository
  {
    private readonly ApplicationDBContext _context = context;

    public async Task<Dog> CreateAsync(Dog dogModel)
    {
      await _context.Dogs.AddAsync(dogModel);
      await _context.SaveChangesAsync();

      return dogModel;
    }
  }
}
