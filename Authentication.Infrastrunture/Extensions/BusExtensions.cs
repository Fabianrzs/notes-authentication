using Common.Communication.Messages;
using Common.Communication.RabbitMQ.RabbitMQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Infrastrunture.Extensions;

public static class BusExtensions
{
    public static void AddServiceBusIntegrationPublisher(this IServiceCollection serviceCollection,
    IConfiguration configuration)
    {
        serviceCollection.AddRabbitMQ(configuration);
        serviceCollection.AddRabbitMQPublisher<IntegrationMessage>();
    }
}

