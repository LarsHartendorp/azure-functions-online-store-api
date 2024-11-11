using OnlineStoreApi.Models.DTO.Requests.User;
using OnlineStoreApi.Models.DTO.Responses.User;
using OnlineStoreAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStoreAPI.Services
{
    public interface IUserService
    {
        Task<IEnumerable<GetUserByIdResponse>> GetUsersWithResponsesAsync();
        Task<GetUserByIdResponse?> GetUserResponseByIdAsync(string id);
        Task<CreateUserResponse> AddUserAsync(CreateUserRequest request);
        Task UpdateUserAsync(string userId, string email);
        Task DeleteUserAsync(string id);
        Task<bool> UserExistsAsync(string id);

    }
}
