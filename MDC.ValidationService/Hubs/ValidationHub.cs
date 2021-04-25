using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace MDC.ValidationService.Hubs
{
    public class ValidationHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            await SendMessage($"{DateTime.Now:MM/dd/yyyy hh:mm:ss.fff tt}: Ping from Validation Service");
        }

        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("broadcastMessage", Context.User?.Identity?.Name, message);
        }
    }
}
