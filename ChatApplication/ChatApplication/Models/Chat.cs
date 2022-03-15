using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApplication.Models
{
    public class Chat
    {
        public int id { get; set; }
        [Display(Name = "Сообщение")]
        public string msg { get; set; }
       
        public int personID { get; set; }
        public Person Person { get; set; }
        [Display(Name = "Дата")]
        public DateTime d_time { get; set; }





        //[Column(TypeName = "datetime")]
        //public DateTime Timestamp { get; set; }
    }
}
