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
        
        services.AddSingleton(new QueueClient(connectionString, orderQueueName));

        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        
        services.AddSingleton<UpdateShippingService>();
        services.AddHttpClient();
    })
    .Build();

host.Run();