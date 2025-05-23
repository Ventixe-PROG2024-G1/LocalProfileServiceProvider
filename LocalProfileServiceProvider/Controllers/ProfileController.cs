using LocalProfileServiceProvider.Data.DTOs;
using LocalProfileServiceProvider.Data.Models;
using LocalProfileServiceProvider.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LocalProfileServiceProvider.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController(UserProfileService profileService) : ControllerBase
    {
        public readonly UserProfileService _profileService = profileService;

        [HttpPost("create-profile")]
        public async Task<ActionResponseRest> CreateProfile([FromBody] CreateProfileRequestRest profileRequest)
        {
            if (!ModelState.IsValid)
            {
                return new ActionResponseRest { Succeeded = false, Error = "Invalid data input" };
            }

            var result = await _profileService.CreateProfile(profileRequest);
            return result;
        }

        [HttpGet("get-profile/{id}")]
        public async Task<ProfileResponseRest> GetProfile(string id)
        {
            if (!ModelState.IsValid)
            {
                return new ProfileResponseRest { Error = "Invalid data input" };
            }

            var result = await _profileService.GetProfile(new GetProfileRequestRest { Id = id });

            if (result == null)
            {
                return new ProfileResponseRest { Error = "Profile not found." };
            }

            return result;
        }

        [HttpGet("get-all-profiles")]
        public async Task<AllProfilesResponseRest> GetAllProfiles()
        {
            var result = await _profileService.GetAllProfiles();

            if (result == null || result.Profiles.Count == 0)
            {
                return new AllProfilesResponseRest { Error = "No profiles found." };
            }

            return result;
        }
    }
}