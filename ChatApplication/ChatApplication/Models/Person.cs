using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApplication.Models
{
    public class Person
    {
        public int id { get; set;}
        [Display(Name = "Имя пользователя")]
        public string name { get; set; }
        [Display(Name = "Пароль")]
        public string passw { get; set; }
        [Display(Name = "Сообщения")]
        public ICollection<Chat> Chats { get; set; }
        public Person()
        {
            Chats = new List<Chat>();
        }




    }
}
