using Microsoft.Extensions.Logging;
using OrderQueueTrigger.Models;

namespace OrderQueueTrigger.Services;

public class UpdateShippingService
{
    private readonly ILogger<UpdateShippingService> _logger;

    public UpdateShippingService(ILogger<UpdateShippingService> logger)
    {
        _logger = logger;
    }

    public async Task UpdateShippingDate(Order order)
    {
        if (order?.shippingDate == null)
        {
            

            _logger.LogInformation($"Order {order.orderId} shipping date updated to {order.shippingDate}");
            // Here, you would add the actual update logic (e.g., updating the order in a database)
            await Task.CompletedTask;
        }
        else
        {
            _logger.LogInformation($"Order {order.orderId} already has a shipping date.");
        }
    }
}