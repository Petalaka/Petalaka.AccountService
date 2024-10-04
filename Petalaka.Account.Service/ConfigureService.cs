using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Petalaka.Account.Core.Utils;
using Petalaka.Account.Service.Events.AccountEvent;

namespace Petalaka.Account.Service;

public static class ConfigureService
{
    public static void AddConfigureServiceService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDependencyInjectionService(configuration);
        services.AddMasstransitRabbitmq(configuration);

    }

    public static void AddMasstransitRabbitmq(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(config =>
        {
            //config rabbitmq host
            config.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(configuration["RabbitMq:Host"], ushort.Parse(configuration["RabbitMq:Port"]),
                    configuration["RabbitMq:VirtualHost"], h =>
                    {
                        h.Username(configuration["RabbitMq:Username"]);
                        h.Password(configuration["RabbitMq:Password"]);
                    });
                //cfg.ConfigureEndpoints(context);
                /*cfg.ReceiveEndpoint("EmailVerification-queue", e =>
                {
                    e.Bind<EmailVerificationEvent>();
                });*/
                /*cfg.Message<EmailVerificationEvent>(e =>
                {
                    e.SetEntityName("EmailVerification-queue"); // Set the queue name for this message type
                });*/
            });
            
        });
        services.AddMassTransitHostedService();
     


    }
}