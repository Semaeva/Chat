using ChatApplication.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApplication.ChatHub
{
    public class Chathub : Hub
    {
        ApplicationContext db;
        public Chathub(ApplicationContext context)
        {
            db = context;
        }

/*        public async Task Send(string message, string userName)
        {
          
            await Clients.All.SendAsync("Send", message, userName);

            await Clients.Caller.SendAsync("Notify1", "Пользователь зашел в  чат");
            await Clients.Others.SendAsync("Receive", $"Имя: {db.Persons} в {DateTime.Now.ToShortTimeString()}");

           // await Clients.AllExcept(new List<string> { Context.ConnectionId }).SendAsync("Receive", $"Добавлено: {} в {DateTime.Now.ToShortTimeString()}");

        }


        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("Notify", $"{Context.ConnectionId} вошел в чат");
            await base.OnConnectedAsync();

        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Clients.All.SendAsync("Notify", $"{Context.ConnectionId} покинул в чат");
            await base.OnDisconnectedAsync(exception);
        }*/
    }
}
