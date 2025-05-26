using Grpc.Core;
using LocalProfileServiceProvider.Data.DTOs;
using LocalProfileServiceProvider.Data.Models;
using LocalProfileServiceProvider.Data.Repositories;
using LocalProfileServiceProvider.Factories;
using static Grpc.Core.Metadata;

namespace LocalProfileServiceProvider.Services
{
    public class UserProfileService(IUserProfileRepo userProfileRepo)
    {
        private readonly IUserProfileRepo _userProfileRepo = userProfileRepo;

        public async Task<ActionResponseRest> CreateProfile(CreateProfileRequestRest request)
        {
            var userProfileEntity = ProfileFactory.Map(request);
            var result = await _userProfileRepo.AddAsync(userProfileEntity);
            if (!result)
            {
                return new ActionResponseRest { Succeeded = false, Error = "Unable to add profile to database" };
            }
            return new ActionResponseRest { Succeeded = true, };
        }

        public async Task<ProfileResponseRest> GetProfile(GetProfileRequestRest request)
        {
            var entity = await _userProfileRepo.GetByIdAsync(request.Id);

            if (entity == null)
            {
                return new ProfileResponseRest { Error = "Profile not found" };
            }

            //Gör factory
            //var userProfile =

            return new ProfileResponseRest
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
        }

        public async Task<AllProfilesResponseRest> GetAllProfiles()
        {
            var entities = await _userProfileRepo.GetAllAsync();
            if (!entities.Any())
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Profiles not found"));
            }

            var response = new AllProfilesResponseRest();

            foreach (var entity in entities)
            {
                //Gör factory
                //var userProfile =
                var profileResponse = new ProfileResponseRest
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

                response.Profiles.Add(profileResponse);
            }

            return response;
        }

        //public override async Task<ProfileResponse> GetProfile(GetProfileRequest request, ServerCallContext context)
        //{
        //    var entity = await _userProfileRepo.GetByIdAsync(request.Id);

        //    if (entity == null)
        //    {
        //        throw new RpcException(new Status(StatusCode.NotFound, "Profile not found"));
        //    }

        //    //Gör factory
        //    //var userProfile =

        //    return new ProfileResponse
        //    {
        //        Id = entity.Id,
        //        FirstName = entity.FirstName,
        //        LastName = entity.LastName,
        //        StreetAddress = entity.StreetAddress,
        //        ZipCode = entity.ZipCode,
        //        City = entity.City,
        //        ProfilePictureUrl = entity.ProfilePictureUrl,
        //        Phone = entity.Phone,
        //    };
        //}

        //public override async Task<AllProfilesResponse> GetAllProfiles(GetAllProfilesRequest request, ServerCallContext context)
        //{
        //    var entities = await _userProfileRepo.GetAllAsync();
        //    if (!entities.Any())
        //    {
        //        throw new RpcException(new Status(StatusCode.NotFound, "Profiles not found"));
        //    }

        //    var response = new AllProfilesResponse();

        //    foreach (var entity in entities)
        //    {
        //        //Gör factory
        //        //var userProfile =
        //        var profileResponse = new ProfileResponse
        //        {
        //            Id = entity.Id,
        //            FirstName = entity.FirstName,
        //            LastName = entity.LastName,
        //            StreetAddress = entity.StreetAddress,
        //            ZipCode = entity.ZipCode,
        //            City = entity.City,
        //            ProfilePictureUrl = entity.ProfilePictureUrl,
        //            Phone = entity.Phone,
        //        };

        //        response.Profiles.Add(profileResponse);
        //    }

        //    return response;
        //}
    }
}