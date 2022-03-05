using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ChatApplication.Models;
using Microsoft.AspNetCore.SignalR;
using ChatApplication.ChatHub;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace ChatApplication.Controllers
{
    public class HomeController : Controller
    {
        IHubContext<Chathub> hubContext;
        ApplicationContext db;
        public HomeController(ApplicationContext context, IHubContext<Chathub> hubContext)
        {
            db = context;
            this.hubContext = hubContext;
        }


        [Authorize]
        public IActionResult Index(int id)
        {
            id = 1;
             var us=   User.Identity.Name;
            ViewBag.id = id;
           var list= db.Person.ToList();
     
            return View(list);
        }

        public async Task<IActionResult> Chat(int id)
        {
            ViewBag.id = id;
            return View();
        }

        [HttpPost] 
        public async Task<IActionResult> Chat(string msg, string notify, int id, string data) //общий чат
        {   
            var name = db.Person.Find(id).name;
             await hubContext.Clients.All.SendAsync("Notify", $"{name} отправил сообщение: {msg} в {DateTime.Now.ToShortTimeString()}");
           // await hubContext.Clients.Client(connectionId).SendAsync("PrivateChat", data);
            return View();
        }


        public async Task<IActionResult> PrivateChat(int id)
        {
            
            ViewBag.id = id;
            return View();
        }


        string groupname = "cats";

        [HttpPost]
        public async Task<IActionResult> PrivateChat(string name, string message, string connID)
         /*   string msg, string notify, int id, string data
        , List<Chat> chat, string connectionId) //личный чат*/
        {
            //  var name = db.Person.Find(id).name;
            // await hubContext.Clients.All.SendAsync("Notify", $"{name} отправил сообщение: {msg} в {DateTime.Now.ToShortTimeString()}");
            //  await hubContext.Clients.Client(connectionId).SendAsync("PrivateChat", data);

            //  await hubContext.Clients.Client(connID).SendAsync(name, message);

/*
            await hubContext.Groups.AddToGroupAsync(db.ConnectionId, groupname);
            await hubContext.Clients.Group(groupname).SendAsync("Notify", $"{username} вошел в чат");*/
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
