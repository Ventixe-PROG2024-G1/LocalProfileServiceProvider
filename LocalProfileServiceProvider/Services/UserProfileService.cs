using Grpc.Core;
using LocalProfileServiceProvider.Data.Repositories;
using LocalProfileServiceProvider.Factories;
using static Grpc.Core.Metadata;

namespace LocalProfileServiceProvider.Services
{
    public class UserProfileService(IUserProfileRepo userProfileRepo) : ProfileContract.ProfileContractBase
    {
        private readonly IUserProfileRepo _userProfileRepo = userProfileRepo;

        public override async Task<ActionResponse> CreateProfile(CreateProfileRequest request, ServerCallContext context)
        {
            var userProfileEntity = ProfileFactory.Map(request);
            var result = await _userProfileRepo.AddAsync(userProfileEntity);
            if (!result)
            {
                return new ActionResponse { Succeeded = false, Error = "Unable to add profile to database" };
            }
            return new ActionResponse { Succeeded = true, };
        }

        public override async Task<ProfileResponse> GetProfile(GetProfileRequest request, ServerCallContext context)
        {
            var entity = await _userProfileRepo.GetByIdAsync(request.Id);

            if (entity == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Profile not found"));
            }

            //Gör factory
            //var userProfile =

            return new ProfileResponse
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                StreetAddress = entity.StreetAddress,
                ZipCode = entity.ZipCode,
                City = entity.City,
                ProfilePictureUrl = entity.ProfilePictureUrl,
            };
        }

        public override async Task<AllProfilesResponse> GetAllProfiles(GetAllProfilesRequest request, ServerCallContext context)
        {
            var entities = await _userProfileRepo.GetAllAsync();
            if (!entities.Any())
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Profiles not found"));
            }

            var response = new AllProfilesResponse();

            foreach (var entity in entities)
            {
                //Gör factory
                //var userProfile =
                var profileResponse = new ProfileResponse
                {
                    Id = entity.Id,
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    StreetAddress = entity.StreetAddress,
                    ZipCode = entity.ZipCode,
                    City = entity.City,
                    ProfilePictureUrl = entity.ProfilePictureUrl,
                };

                response.Profiles.Add(profileResponse);
            }

            return response;
        }
    }
}