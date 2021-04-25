using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace MDC.ContributionsService.Messaging.RabbitMq
{
    public static class RabbitListenersInstaller
    {
        public static void UseRabbitListeners(this IApplicationBuilder app, List<Type> eventTypes)
        {
            app.ApplicationServices.GetRequiredService<RabbitEventListener>().ListenTo(eventTypes);
        }
    }
}