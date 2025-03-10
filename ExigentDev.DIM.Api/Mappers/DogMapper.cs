using ExigentDev.DIM.Api.Dtos.Dog;
using ExigentDev.DIM.Api.Models;

namespace ExigentDev.DIM.Api.Mappers
{
  public static class DogMapper
  {
    public static DogDto ToDogDto(this Dog dogModel)
    {
      return new DogDto
      {
        Id = dogModel.Id,
        DateMet = dogModel.DateMet,
        Rating = dogModel.Rating,
        Comment = dogModel.Comment,
        Breed = dogModel.Breed,
        Name = dogModel.Name,
        DogImages = [.. dogModel.DogImages.Select(dogImage => dogImage.ToDogImageDto())],
      };
    }
  }
}
