using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApplication.Models
{
    public class Person
    {
        public int id { get; set;}
        public string name { get; set; }
        public string passw { get; set; }

        public ICollection<Chat> Chats { get; set; }
        public Person()
        {
            Chats = new List<Chat>();
        }
        public string ConnectionId { get; set; }


        //public Chat Chat { get; set; }
        //public List<Chat> Lines { get; set; }
    }
}
