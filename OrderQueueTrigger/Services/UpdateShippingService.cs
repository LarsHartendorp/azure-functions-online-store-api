using Microsoft.Extensions.Logging;
using OrderQueueTrigger.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OrderQueueTrigger.Services
{
    public class UpdateShippingService
    {
        private readonly ILogger<UpdateShippingService> _logger;
        private readonly HttpClient _httpClient;

        public UpdateShippingService(ILogger<UpdateShippingService> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        public async Task UpdateShippingDate(Order order)
        {
            _logger.LogInformation($"Attempting to update shipping date for Order {order.orderId}");

            if (order?.shippingDate == null)
            {
                // Set the shipping date to the current date
                var newShippingDate = DateTime.UtcNow;
                order.shippingDate = newShippingDate;

                // Construct the request payload
                var requestPayload = new
                {
                    shippingDate = newShippingDate
                };

                // Serialize the payload
                var content = new StringContent(JsonSerializer.Serialize(requestPayload), Encoding.UTF8, "application/json");

                // Make the PUT request to the OrdersController
                var url = $"http://localhost:5252/api/Orders/{order.orderId}";
                var response = await _httpClient.PutAsync(url, content);
                _logger.LogInformation($"Response status: {response.StatusCode}");
                var responseBody = await response.Content.ReadAsStringAsync();
                _logger.LogInformation($"Response body: {responseBody}");


                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation($"Order {order.orderId} shipping date updated to {order.shippingDate}");
                }
                else
                {
                    _logger.LogError($"Failed to update shipping date for Order {order.orderId}. Status code: {response.StatusCode}");
                }
            }
            else
            {
                _logger.LogInformation($"Order {order.orderId} already has a shipping date.");
            }
        }
    }
}
