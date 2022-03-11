using ChatApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChatApplication.ChatHub
{

    [Authorize]
    public class Chathub : Hub
    {



        ApplicationContext db;
        public Chathub(ApplicationContext context)
        {
            db = context;
        }

        public static class PerosnHandler
        {
            //public static List<string> ConnectedIds = new List<string>();
            public static List<string> ConnectedIds = new List<string>();

        }



        string groupname = "cats";

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var name = Context.User.Identity.Name;

             PerosnHandler.ConnectedIds.Remove(name);
            await base.OnDisconnectedAsync(exception);
        }
        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupname);
    
           var name = Context.User.Identity.Name;
            
            PerosnHandler.ConnectedIds.Add(name);
            var dupl = PerosnHandler.ConnectedIds.Distinct().ToList();

            var list = JsonSerializer.Serialize(dupl);

           
            await Clients.Group(groupname).SendAsync("PerosnHandler", $"{list}");

            await Clients.Group(groupname).SendAsync("Notify", $"{name} вошел в чат");
            await base.OnConnectedAsync();
        }


        public async Task Send(string message, string name)
        {
             name = Context.User.Identity.Name.ToString();
            var personId = db.Person
            .Where(c => c.name == name)
            .Select(s => s.id)
            .FirstOrDefault();
            Chat chat = new Chat
            {
                msg = message,
                personID = personId,
                date_time = DateTime.Now.ToShortTimeString().ToString(),
            };

            db.Chat.Add(chat);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
           
            await Clients.Group(groupname).SendAsync("Receive", message, name);
        }

    }

  
}
