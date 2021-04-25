using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using RawRabbit.DependencyInjection.ServiceCollection;
using RawRabbit.Instantiation;

namespace MDC.ValidationService.Messaging.RabbitMq
{
    public static class RawRabbitInstaller
    {
        public static IServiceCollection AddRabbitListeners(this IServiceCollection services)
        {
            services.AddRawRabbit(new RawRabbitOptions
            {
                ClientConfiguration = new RawRabbit.Configuration.RawRabbitConfiguration
                {
                    //todo: credentials in code - sort out
                    Username = "guest",
                    Password = "guest",
                    VirtualHost = "/",
                    Port = 5672,
                    Hostnames = new List<string> { "localhost" },
                    RequestTimeout = TimeSpan.FromSeconds(10),
                    PublishConfirmTimeout = TimeSpan.FromSeconds(1),
                    RecoveryInterval = TimeSpan.FromSeconds(1),
                    PersistentDeliveryMode = true,
                    AutoCloseConnection = true,
                    AutomaticRecovery = true,
                    TopologyRecovery = true,
                    Exchange = new RawRabbit.Configuration.GeneralExchangeConfiguration
                    {
                        Durable = true,
                        AutoDelete = false,
                        Type = RawRabbit.Configuration.Exchange.ExchangeType.Topic
                    },
                    Queue = new RawRabbit.Configuration.GeneralQueueConfiguration
                    {
                        Durable = true,
                        AutoDelete = false,
                        Exclusive = false
                    }
                }
            });

            services.AddSingleton(svc => new RabbitEventListener(svc.GetRequiredService<IBusClient>(), svc));

            return services;
        }
    }
}
