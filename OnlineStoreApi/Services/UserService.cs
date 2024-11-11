using MongoDB.Bson;
using OnlineStoreApi.Models.DTO.Requests.User;
using OnlineStoreApi.Models.DTO.Responses.User;
using OnlineStoreAPI.Models;
using OnlineStoreAPI.Repositories;

namespace OnlineStoreAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public UserService(IUserRepository userRepository, IProductRepository productRepository, IOrderRepository orderRepository)
        {
            _userRepository = userRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<GetUserByIdResponse>> GetUsersWithResponsesAsync()
        {
            var users = await _userRepository.GetUsersAsync();
            var responses = users.Select(user => new GetUserByIdResponse
            {
                UserId = user.UserId.ToString(),
                Email = user.Email,
            });

            return responses;
        }

        public async Task<GetUserByIdResponse?> GetUserResponseByIdAsync(string id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return null;
            }

            // Retrieve user's orders (this assumes a method GetOrdersByUserIdAsync in your repository)
            var userOrders = await _orderRepository.GetOrdersByUserIdAsync(id);

            var orders = new List<GetUserByIdOrderResponse>();
            foreach (var order in userOrders)
            {
                // Retrieve products for each order (this assumes a method GetProductsByOrderIdAsync in your repository)
                var orderProducts = order.Products;

                var products = orderProducts.Select(product => new GetProductsByIdResponse
                {
                    ProductId = product.ProductId.ToString(),
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    ImageUrl = product.ImageUrl
                }).ToList();

                orders.Add(new GetUserByIdOrderResponse
                {
                    OrderId = order.OrderId.ToString(),
                    OrderDate = order.OrderDate,
                    ShippingDate = order.ShippingDate,
                    Products = products
                });
            }

            return new GetUserByIdResponse
            {
                UserId = user.UserId.ToString(),
                Email = user.Email,
                Orders = orders
            };
        }

        public async Task<CreateUserResponse> AddUserAsync(CreateUserRequest request)
        {
            User user = new User
            {
                UserId = ObjectId.GenerateNewId(),
                Email = request.Email,
            };

            await _userRepository.AddUserAsync(user);

            return new CreateUserResponse
            {
                UserId = user.UserId.ToString(),
                Email = user.Email,
            };
        }

        public async Task UpdateUserAsync(string userId, string email)
        {
            await _userRepository.UpdateUserAsync(userId, email);
        }

        public async Task DeleteUserAsync(string id)
        {
            await _userRepository.DeleteUserAsync(id);
        }

        public async Task<bool> UserExistsAsync(string id)
        {
            return await _userRepository.UserExistsAsync(id);
        }

    }
}
