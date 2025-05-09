using LocalProfileServiceProvider.Data.Entities;

namespace LocalProfileServiceProvider.Data.Repositories
{
    public interface IUserProfileRepo
    {
        Task<bool> AddAsync(UserProfileEntity entity);

        Task DeleteAsync(string id);

        Task<IEnumerable<UserProfileEntity>> GetAllAsync();

        Task<UserProfileEntity?> GetByIdAsync(string id);

        Task UpdateAsync(UserProfileEntity entity);
    }
}