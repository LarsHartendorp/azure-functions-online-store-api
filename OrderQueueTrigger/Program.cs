using Azure.Storage.Queues;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using OrderQueueTrigger.Services;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        var connectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
        var orderQueueName = Environment.GetEnvironmentVariable("OrdersQueue");
        
        // Register QueueClient for stationqueue
        services.AddSingleton(new QueueClient(connectionString, orderQueueName));

        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        // Register UpdateShippingService
        services.AddSingleton<UpdateShippingService>();
        services.AddHttpClient();
    })
    .Build();

host.Run();