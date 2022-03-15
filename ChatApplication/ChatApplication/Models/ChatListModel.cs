using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ChatApplication.Models
{
    public class ChatListModel 
    {

        public IEnumerable<Chat> Chats { get; set; }
        public List<SelectListItem> Persons { get; set; }
        public string NamePerson { get; set; }
    }
}
