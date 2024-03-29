﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ChatApplication.Models;
using Microsoft.AspNetCore.SignalR;
using ChatApplication.ChatHub;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Security.Principal;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public IActionResult ChatHistory(string sortOrder, string dateBegin, string dateEnd)
        {
            ViewData["NameSort"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSort"] = sortOrder == "date_time" ? "date_desc" : "date_time";
            var sorting = from s in db.Chat
                           select s;
            switch (sortOrder)
            {
                case "name_desc":
                    sorting = sorting.OrderByDescending(s => s.Person.name);
                    break;
                case "date_time":
                    sorting = sorting.OrderBy(s => s.d_time);//по времени
                    break;
                case "date_desc":
                    sorting = sorting.OrderByDescending(s => s.d_time);
                    break;
                default:
                    sorting = sorting.OrderBy(s => s.Person.name);
                    break;
            }

            var list=  sorting.Include(s => s.Person).ToList();
            return View(list);
        }


        [HttpPost]
        public IActionResult ChatHistory(DateTime dateBegin, DateTime dateEnd, string searcSrtring)//фильтрация по дате и фильрация по имени пользователя в чате
        {
            var list = from s in db.Chat
                       select s;
            try
            {
                if (searcSrtring !=null)
            {
                var list_ = list.Where(x=>x.Person.name == searcSrtring)
                       .Include(s => s.Person).ToList();
                return View(list_);
            }
            if (dateBegin != null && dateEnd != null) {
                var list_ = list.Where(x => x.d_time >= dateBegin && x.d_time <= dateEnd)
                        .Include(s => s.Person).ToList();
                return View(list_);
            }
          
                if (dateBegin != null && dateEnd != null && searcSrtring !=null)
            {
                var list_ = list.Where(x => x.d_time >= dateBegin && x.d_time <= dateEnd && x.Person.name == searcSrtring)
                        .Include(s => s.Person).ToList();
                return View(list_);
            }

            }
            catch (Exception ex) { };
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
