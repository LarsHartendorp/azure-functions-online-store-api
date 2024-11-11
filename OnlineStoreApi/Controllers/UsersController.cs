using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStoreApi.Models.DTO.Requests.User;
using OnlineStoreApi.Models.DTO.Responses.Order;
using OnlineStoreApi.Models.DTO.Responses.User;
using OnlineStoreAPI.Models;
using OnlineStoreAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetUserByIdResponse>>> GetUsers()
        {
            var response = await _userService.GetUsersWithResponsesAsync();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetUserByIdResponse>> GetUser(string id)
        {
            var response = await _userService.GetUserResponseByIdAsync(id);
            if (response == null)
            {
                return NotFound(new { message = $"User with ID {id} not found" });
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<CreateUserResponse>> PostUser(CreateUserRequest request)
        {
            var response = await _userService.AddUserAsync(request);
            return CreatedAtAction(nameof(GetUser), new { id = response.UserId }, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PutUserByIdResponse>> PutUser(string id, PutUserByIdRequest request)
        {
            // Check if the user exists
            var exists = await _userService.UserExistsAsync(id);
            if (!exists)
            {
                return NotFound(new { message = $"User with ID {id} not found" });
            }

            // Update the user's email
            await _userService.UpdateUserAsync(id, request.Email);

            // Retrieve the updated user response
            var updatedResponse = await _userService.GetUserResponseByIdAsync(id);
            return Ok(updatedResponse);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(string id)
        {
            // Check if the user exists
            var exists = await _userService.UserExistsAsync(id);
            if (!exists)
            {
                return NotFound(new { message = $"User with ID {id} not found" });
            }

            await _userService.DeleteUserAsync(id);
            return NoContent();
        }
    }
}
