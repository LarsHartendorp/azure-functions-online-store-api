using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using OnlineStoreApi.HandleOrdersTimeTriggerFunction.Services;

namespace HandleOrdersTimeTriggerFunction;

public class TimerTrigger
{
    private readonly ILogger<TimerTrigger> _logger;
    private readonly OrderService _orderService;

    public TimerTrigger(ILogger<TimerTrigger> logger, OrderService orderService)
    {
        _logger = logger;
        _orderService = orderService;  
    }

    [Function("TimerTrigger")]
    public async Task Run([TimerTrigger("0 0 0 * * *")] TimerInfo myTimer)
    {
        _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        await _orderService.HandleTimeTrigger();
    }
    
    // Timer trigger for testing every minute
    // [Function("TimerTrigger")]
    // public async Task Run([TimerTrigger("0 * * * * *")] TimerInfo myTimer)
    // {
    //     _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
    //     await _orderService.HandleTimeTrigger();
    // }
}