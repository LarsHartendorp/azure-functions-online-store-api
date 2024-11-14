using Azure.Storage.Queues;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using OnlineStoreApi.HandleOrdersTimeTriggerFunction.Services;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        
        var connectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
        var queueName = Environment.GetEnvironmentVariable("OrdersQueue");
        
        services.AddHttpClient<OrderService>(client =>
        {
            client.BaseAddress = new Uri("http://localhost:5252/");
        });
        services.AddSingleton(new QueueClient(connectionString, queueName));

        services.AddScoped<OrderService>();
    })
    .Build();

host.Run(); 