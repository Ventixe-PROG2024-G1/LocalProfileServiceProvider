using LocalProfileServiceProvider.Data.DTOs;
using LocalProfileServiceProvider.Data.Entities;
using LocalProfileServiceProvider.Data.Models;
using LocalProfileServiceProvider.Services;
using static Grpc.Core.Metadata;

namespace LocalProfileServiceProvider.Factories
{
    public class ProfileFactory
    {
        public static UserProfileEntity Map(CreateProfileRequestRest request)
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
                Phone = request.Phone,
            };

            return userProfileEntity;
        }

        public static ProfileResponseRest Map(UserProfileEntity entity)
        {
            var response = new ProfileResponseRest
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                StreetAddress = entity.StreetAddress,
                ZipCode = entity.ZipCode,
                City = entity.City,
                ProfilePictureUrl = entity.ProfilePictureUrl,
                Phone = entity.Phone,
            };

            return response;
        }
    }
}