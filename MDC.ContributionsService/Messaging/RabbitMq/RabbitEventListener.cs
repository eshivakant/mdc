using System;
using System.Collections.Generic;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using RawRabbit.Pipe;

namespace MDC.ContributionsService.Messaging.RabbitMq
{
    public class RabbitEventListener
    {
        private readonly IBusClient _busClient;
        private readonly IServiceProvider _serviceProvider;

        public RabbitEventListener(
            IBusClient busClient,
            IServiceProvider serviceProvider)
        {
            this._busClient = busClient;
            this._serviceProvider = serviceProvider;
        }

        public void ListenTo(List<Type> eventsToSubscribe)
        {
            foreach (var evtType in eventsToSubscribe)
            {
                //add check if is INotification
                GetType()
                    .GetMethod("Subscribe", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                    ?.MakeGenericMethod(evtType)
                    .Invoke(this, new object[] { });
            }
        }

        private void Subscribe<T>() where T : INotification
        {
            this._busClient.SubscribeAsync<T>(
                async (msg) =>
                {
                    //add logging
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var internalBus = scope.ServiceProvider.GetRequiredService<IMediator>();
                        await internalBus.Publish(msg);
                    }
                },
                cfg => cfg.UseConsumerConfiguration(
                    c => c
                        .OnDeclaredExchange(e => e
                            .WithName("mdc-api-gateway")
                            .WithType(RawRabbit.Configuration.Exchange.ExchangeType.Topic)
                            .WithArgument("key", typeof(T).Name.ToLower()))
                        .FromDeclaredQueue(q => q.WithName("mdc-api-gateway-service-" + typeof(T).Name)))
            );
        }
    }
}