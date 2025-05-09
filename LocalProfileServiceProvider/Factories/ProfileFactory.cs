using LocalProfileServiceProvider.Data.Entities;
using LocalProfileServiceProvider.Services;

namespace LocalProfileServiceProvider.Factories
{
    public class ProfileFactory
    {
        public static UserProfileEntity Map(CreateProfileRequest request)
        {
            var userProfileEntity = new UserProfileEntity
            {
                Id = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                StreetAddress = request.StreetAddress,
                ZipCode = request.ZipCode,
                City = request.City,
                ProfilePictureUrl = request.ProfilePictureUrl,
            };

            return userProfileEntity;
        }
    }
}