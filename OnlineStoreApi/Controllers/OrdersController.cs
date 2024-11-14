using Microsoft.AspNetCore.Mvc;
using OnlineStoreApi.Models.DTO.Requests.Order;
using OnlineStoreApi.Models.DTO.Responses.Order;
using OnlineStoreAPI.Services;

namespace OnlineStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetOrderByIdResponse>>> GetOrders()
        {
            var response = await _orderService.GetOrdersWithResponsesAsync();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetOrderByIdResponse>> GetOrder(string id)
        {
            var response = await _orderService.GetOrderResponseByIdAsync(id);
            if (response == null)
            {
                return NotFound(new { message = $"Order with ID {id} not found" });
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<CreateOrderResponse>> PostOrder(CreateOrderRequest request)
        {
            var response = await _orderService.AddOrderAsync(request);
            return CreatedAtAction(nameof(GetOrder), new { id = response.OrderId }, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PutOrderByIdResponse>> PutOrder(string id, PutOrderByIdRequest request)
        {
            var exists = await _orderService.OrderExistsAsync(id);
            if (!exists)
            {
                return NotFound(new { message = $"Order with ID {id} not found" });
            }
            
            var shippingDate = request.ShippingDate ?? DateTime.Now;
            await _orderService.UpdateOrderAsync(id, shippingDate);
            
            var updatedResponse = await _orderService.GetOrderResponseByIdAsync(id);

            return Ok(updatedResponse);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder(string id)
        {
            var exists = await _orderService.OrderExistsAsync(id);
            if (!exists)
            {
                return NotFound();
            }

            await _orderService.DeleteOrderAsync(id);
            return NoContent();
        }
    }
}
