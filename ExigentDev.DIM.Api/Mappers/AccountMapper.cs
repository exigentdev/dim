using ExigentDev.DIM.Api.Dtos.Account;
using ExigentDev.DIM.Api.Models;

namespace ExigentDev.DIM.Api.Mappers
{
  public static class AccountMapper
  {
    public static AppUserDto ToAppUserDto(this AppUser appUserModel)
    {
      return new AppUserDto
      {
        UserName = appUserModel.UserName!,
        DateJoined = appUserModel.DateJoined,
        ProfileImageUrl = appUserModel.ProfileImageUrl,
      };
    }
  }
}
