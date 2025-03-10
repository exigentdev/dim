using ExigentDev.DIM.Api.Dtos.DogImage;
using ExigentDev.DIM.Api.Models;

namespace ExigentDev.DIM.Api.Mappers
{
  public static class DogImageMapper
  {
    public static DogImageDto ToDogImageDto(this DogImage dogImageModel)
    {
      return new DogImageDto
      {
        Id = dogImageModel.Id,
        ImageUrl = dogImageModel.ImageUrl,
        DogId = dogImageModel.DogId,
      };
    }
  }
}
