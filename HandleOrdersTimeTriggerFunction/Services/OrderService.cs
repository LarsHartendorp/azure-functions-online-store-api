using System.Text;
using System.Text.Json;
using Azure.Storage.Queues;
using HandleOrdersTimeTriggerFunction.Models;

namespace OnlineStoreApi.HandleOrdersTimeTriggerFunction.Services;

public class OrderService
{
    private readonly HttpClient _httpClient;
    private readonly QueueClient _queueClient;

    public OrderService(HttpClient httpClient, QueueClient queueClient)
    {
        _httpClient = httpClient;
        _queueClient = queueClient;
    }

    public async Task<List<Order>> GetOrdersAsync()
    {
        // Fetch orders
        var response = await _httpClient.GetAsync("http://localhost:5252/api/Orders");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        Console.WriteLine(content);
        
        return JsonSerializer.Deserialize<List<Order>>(content);
    }
    
    // Pushes orders without a shipping date to the queue
    public async Task HandleTimeTrigger()
    {
        var orders = await GetOrdersAsync();

        // Ensure queue exists
        await _queueClient.CreateIfNotExistsAsync();

        foreach (var order in orders)
        {
            if (order.shippingDate == null)
            {
                var orderJson = JsonSerializer.Serialize(order);
                var encodedMessage = Convert.ToBase64String(Encoding.UTF8.GetBytes(orderJson));
                await _queueClient.SendMessageAsync(encodedMessage);
            }
        }
    }
}