using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace ChatApplication.Models
{
    public class LoginModel
    {

        [Required(ErrorMessage = "Не указан Email")]
        public string name { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string passw { get; set; }
    }
}
