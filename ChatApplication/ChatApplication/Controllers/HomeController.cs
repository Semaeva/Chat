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

        ApplicationContext db;

        public HomeController(ApplicationContext context)
        {
            db = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            var list= db.Person.ToList();
            return View(list);
        }


        [Authorize]
        public IActionResult ChatHistory()
        {
            var list = db.Chat.Include(p=>p.Person).ToList();
            return View(list);
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
