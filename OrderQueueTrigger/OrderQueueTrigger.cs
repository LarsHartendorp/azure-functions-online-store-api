using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using OrderQueueTrigger.Models;
using OrderQueueTrigger.Services;

namespace OrderQueueTrigger;

public class OrderQueueTrigger
{
    private readonly ILogger<OrderQueueTrigger> _logger;
    private readonly UpdateShippingService _updateShippingService;
    private readonly HttpClient _httpClient;

    public OrderQueueTrigger(ILogger<OrderQueueTrigger> logger, UpdateShippingService updateShippingService, HttpClient httpClient)
    {
        _logger = logger;
        _updateShippingService = updateShippingService;
        _httpClient = httpClient;
    }

    [Function(nameof(OrderQueueTrigger))]
    public async Task RunAsync([QueueTrigger("ordersqueue", Connection = "AzureWebJobsStorage")] string queueMessage)
    {
        _logger.LogInformation($"Queue message received: {queueMessage}");

        Order order;

        try
        {
            order = JsonSerializer.Deserialize<Order>(queueMessage);
            if (order == null)
            {
                _logger.LogError("Deserialization failed, order is null.");
                return;
            }
        }
        catch (JsonException ex)
        {
            _logger.LogError($"Deserialization error: {ex.Message}");
            return;
        }

        // Update the shipping date using the service
        await _updateShippingService.UpdateShippingDate(order);
    }
}