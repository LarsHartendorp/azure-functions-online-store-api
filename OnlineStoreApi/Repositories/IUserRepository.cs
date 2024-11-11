using OnlineStoreAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStoreAPI.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User?> GetUserByIdAsync(string id);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(string userId, string email);
        Task DeleteUserAsync(string id);
        Task<bool> UserExistsAsync(string id);

    }
}
