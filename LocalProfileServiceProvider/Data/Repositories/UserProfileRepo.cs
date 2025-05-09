using LocalProfileServiceProvider.Data.Contexts;
using LocalProfileServiceProvider.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace LocalProfileServiceProvider.Data.Repositories
{
    public class UserProfileRepo(ProfileDbContext context) : IUserProfileRepo
    {
        private readonly ProfileDbContext _context = context;
        private readonly DbSet<UserProfileEntity> _dbSet = context.Set<UserProfileEntity>();

        public async Task<UserProfileEntity?> GetByIdAsync(string id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<UserProfileEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<bool> AddAsync(UserProfileEntity entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task UpdateAsync(UserProfileEntity entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}