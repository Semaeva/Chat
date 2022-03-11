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
using System.Security.Claims;
using System.Security.Principal;

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
        public IActionResult Index()
        {
            
            var list= db.Person.ToList();
     
            return View(list);
        }

        public async Task<IActionResult> Chat()
        {
            var name= User.Identity.Name.ToString();
            ViewBag.name = name;

            var personId = db.Person
       .Where(c => c.name == name)
       .Select(s => s.id)
       .FirstOrDefault();
            ViewBag.id=personId;

            /* var list = db.Chat.Include(u=>u.Person).ToList();
             return View(list);*/
            return View();
        }   

        [HttpPost] 
        public async Task<IActionResult> Chat(string msg, string nameUser, string userId) //общий чат
        {
            //if (Context.UserIdentifier != to) // если получатель и текущий пользователь не совпадают
            //    await Clients.User(Context.UserIdentifier).SendAsync("Receive", message, userName);
            var personId = db.Person
       .Where(c => c.name == nameUser)
       .Select(s => s.id)
       .FirstOrDefault();
            await hubContext.Clients.All.SendAsync("NameUser", $" {personId} ");
            await hubContext.Clients.All.SendAsync("newMsg", $" {msg}");
         //   await hubContext.Clients.All.SendAsync("Send", $" {msg}");

            //   await hubContext.Clients.All.SendAsync("Notify", $" отправил сообщение: {msg} в {DateTime.Now.ToShortTimeString()}");


            //    var personId = db.Person
            //.Where(c => c.name == nameUser)
            //.Select(s => s.id)
            //.FirstOrDefault();
            // Создать нового покупателя
            Chat chat = new Chat { 
                msg = msg,
                personID = personId,
                date_time = DateTime.Now.ToShortTimeString().ToString(),
              // Person= person
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
