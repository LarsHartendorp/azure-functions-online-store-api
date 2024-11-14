using OnlineStoreApi.Models.DTO.Requests.Order;
using OnlineStoreApi.Models.DTO.Responses.Order;
using OnlineStoreAPI.Models;

namespace OnlineStoreAPI.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<GetOrderByIdResponse>> GetOrdersWithResponsesAsync();
        Task<GetOrderByIdResponse?> GetOrderResponseByIdAsync(string id);
        Task<CreateOrderResponse> AddOrderAsync(CreateOrderRequest request);
        Task UpdateOrderAsync(string orderId, DateTime shippingDate);
        Task DeleteOrderAsync(string id);
        Task<bool> OrderExistsAsync(string id);
    }
}
