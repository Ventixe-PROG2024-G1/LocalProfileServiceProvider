using LocalProfileServiceProvider.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LocalProfileServiceProvider.Data.Contexts
{
    public class ProfileDbContext(DbContextOptions<ProfileDbContext> options) : DbContext(options)
    {
        public virtual DbSet<UserProfileEntity> UserProfiles { get; set; }
    }
}