using ChatApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
            public static List<string> ConnectedIds = new List<string>();

        }


        string groupname = "myGroup";

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var name = Context.User.Identity.Name;

             PerosnHandler.ConnectedIds.Remove(name);
            await base.OnDisconnectedAsync(exception);
        }
        public override async Task OnConnectedAsync()
        {
            var list_chat =( from s in db.Chat
                            join p in db.Person on s.personID equals p.id
                             orderby s.d_time descending 
                             select new { person = p.name, chat = s.msg, dates = s.d_time }).Where(s=>s.dates >= DateTime.Today.AddDays(-1));
         
            var chats_json = JsonSerializer.Serialize(list_chat);
            await Groups.AddToGroupAsync(Context.ConnectionId, groupname);
    
           var name = Context.User.Identity.Name;
            
            PerosnHandler.ConnectedIds.Add(name);

            var dupl = PerosnHandler.ConnectedIds.Distinct().ToList();
            var list = JsonSerializer.Serialize(dupl);
       
            await Clients.Group(groupname).SendAsync("PerosnHandler", $"{list}");
          await Clients.Group(groupname).SendAsync("ChatsLog", $"{chats_json}");

            await Clients.Group(groupname).SendAsync("Notify", $"{name} вошел в чат");
            await base.OnConnectedAsync();
        }


        public static ConcurrentDictionary<string, List<string>> ConnectedUsers = new ConcurrentDictionary<string, List<string>>();
            
        public async Task Send(string message,string username)
        {     
             username = Context.User.Identity.Name.ToString();
            List<string> existingUserConnectionIds;
            ConnectedUsers.TryGetValue(username, out existingUserConnectionIds);
            if (existingUserConnectionIds == null)
            {
                existingUserConnectionIds = new List<string>();
            }

            existingUserConnectionIds.Add(Context.ConnectionId);
            var conn = Context.ConnectionId;

            var currname = Context.User.Identity.Name;
            var personId = db.Person
            .Where(c => c.name == username)
            .Select(s => s.id)
            .FirstOrDefault();

            var localDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Replace(' ', 'T');
          
            Chat chat = new Chat
            {
                msg = message,
                personID = personId,
                d_time = DateTime.Now
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

          
            await Clients.Group(groupname).SendAsync("Receive", message, username);

        }


    }

  
}
