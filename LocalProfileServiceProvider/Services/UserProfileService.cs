using Grpc.Core;
using LocalProfileServiceProvider.Data.DTOs;
using LocalProfileServiceProvider.Data.Models;
using LocalProfileServiceProvider.Data.Repositories;
using LocalProfileServiceProvider.Factories;
using Microsoft.Extensions.Caching.Memory;
using static Grpc.Core.Metadata;

namespace LocalProfileServiceProvider.Services
{
    public class UserProfileService(IUserProfileRepo userProfileRepo, IMemoryCache cache)
    {
        private readonly IUserProfileRepo _userProfileRepo = userProfileRepo;

        private readonly IMemoryCache _cache = cache;
        private const string _cacheKey_AllProfiles = "UserProfiles_All";

        public async Task<ActionResponseRest> CreateProfile(CreateProfileRequestRest request)
        {
            var userProfileEntity = ProfileFactory.Map(request);
            var result = await _userProfileRepo.AddAsync(userProfileEntity);
            if (!result)
            {
                return new ActionResponseRest { Succeeded = false, Error = "Unable to add profile to database" };
            }
            _cache.Remove(_cacheKey_AllProfiles);
            return new ActionResponseRest { Succeeded = true, };
        }

        public async Task<ProfileResponseRest> GetProfile(GetProfileRequestRest request)
        {
            var entity = await _userProfileRepo.GetByIdAsync(request.Id);

            if (entity == null)
            {
                return new ProfileResponseRest { Error = "Profile not found" };
            }

            return ProfileFactory.Map(entity);
        }

        public async Task<AllProfilesResponseRest> GetAllProfiles()
        {
            if (_cache.TryGetValue(_cacheKey_AllProfiles, out AllProfilesResponseRest cachedProfiles))
            {
                return cachedProfiles;
            }

            var entities = await _userProfileRepo.GetAllAsync();
            if (!entities.Any())
            {
                return new AllProfilesResponseRest { Error = "Profiles not found" };
            }

            var response = new AllProfilesResponseRest();

            foreach (var entity in entities)
            {
                var profileResponse = ProfileFactory.Map(entity);

                response.Profiles.Add(profileResponse);
            }
            _cache.Set(_cacheKey_AllProfiles, response, TimeSpan.FromMinutes(15));
            return response;
        }
    }
}