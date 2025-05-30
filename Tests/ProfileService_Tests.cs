using LocalProfileServiceProvider.Data.Contexts;
using LocalProfileServiceProvider.Data.DTOs;
using LocalProfileServiceProvider.Data.Repositories;
using LocalProfileServiceProvider.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class ProfileService_Tests
    {
        private readonly ServiceProvider _provider;
        private readonly IUserProfileRepo _userProfileRepo;
        private readonly IMemoryCache _cache;
        private readonly UserProfileService _userProfileService;
        private readonly ProfileDbContext _context;

        public ProfileService_Tests()
        {
            var services = new ServiceCollection();

            services.AddDbContext<ProfileDbContext>(x => x.UseInMemoryDatabase($"{Guid.NewGuid()}"));

            services.AddScoped<IUserProfileRepo, UserProfileRepo>();
            services.AddMemoryCache();

            _provider = services.BuildServiceProvider();

            // Initiera tjänster
            _userProfileRepo = _provider.GetRequiredService<IUserProfileRepo>();
            _cache = _provider.GetRequiredService<IMemoryCache>();
            _userProfileService = new UserProfileService(_userProfileRepo, _cache);
            _context = _provider.GetRequiredService<ProfileDbContext>();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _provider.Dispose();
        }

        [Fact]
        public async Task CreateProfile_ShouldReturnSuccess_WhenValidDataProvided()
        {
            // Arrange
            var request = new CreateProfileRequestRest
            {
                Id = "1234",
                FirstName = "John",
                LastName = "Doe",
                StreetAddress = "Street 10",
                ZipCode = "12345",
                City = "Skinnskatteberg",
                Phone = "0706196987"
            };

            // Act
            var response = await _userProfileService.CreateProfile(request);

            // Assert
            Assert.True(response.Succeeded);

            Dispose();
        }

        [Fact]
        public async Task GetProfile_ShouldReturnError_WhenProfileDoesNotExist()
        {
            // Act
            var profileResponse = await _userProfileService.GetProfile(new GetProfileRequestRest { Id = "0000" });

            // Assert
            Assert.NotNull(profileResponse);
            Assert.Equal("Profile not found", profileResponse.Error);
        }

        [Fact]
        public async Task GetAllProfiles_ShouldReturnList_WhenProfilesExist()
        {
            await _userProfileService.CreateProfile(new CreateProfileRequestRest
            {
                Id = "1234",
                FirstName = "John",
                LastName = "Doe",
                StreetAddress = "Street 10",
                ZipCode = "12345",
                City = "Skinnskatteberg",
                Phone = "0706196987"
            });
            await _userProfileService.CreateProfile(new CreateProfileRequestRest
            {
                Id = "5678",
                FirstName = "John",
                LastName = "Doe",
                StreetAddress = "Street 10",
                ZipCode = "12345",
                City = "Skinnskatteberg",
                Phone = "0706196987"
            });

            // Act
            var allProfilesResponse = await _userProfileService.GetAllProfiles();

            // Assert
            Assert.NotNull(allProfilesResponse.Profiles);
            Assert.True(allProfilesResponse.Profiles.Count >= 2);
        }
    }
}