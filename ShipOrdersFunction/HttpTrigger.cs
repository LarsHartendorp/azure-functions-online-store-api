using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using OnlineStoreApi.HandleOrdersTimeTriggerFunction.Services;

namespace ShipOrdersFunction
{
    public class HttpTrigger
    {
        private readonly ILogger<HttpTrigger> _logger;
        private readonly OrderService _orderService;

        public HttpTrigger(ILogger<HttpTrigger> logger, OrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        [Function("ShipOrder")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "api/orders/{orderId}/ship")] HttpRequest req, 
            string orderId)
        {
            _logger.LogInformation($"HTTP trigger function processed a request to ship order {orderId}");

            // Fetch the order using the orderId (you need a method to retrieve the order)
            // var order = await _orderService.GetOrderByIdAsync(orderId); // This method needs to be implemented in OrderService

            var orders = await _orderService.GetOrdersAsync();
            
            // make a foreach to check each order
            foreach (var order in orders)
            {
                if (order == null)
                {
                    return new NotFoundResult(); // Order not found
                }

                // Call the UpdateShippingDate method
                // await _orderService.UpdateShippingDate(order);
            }
            return new OkResult(); // Successfully shipped the order
        }
    }
}