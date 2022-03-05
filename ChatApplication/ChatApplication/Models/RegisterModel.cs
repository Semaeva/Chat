using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ChatApplication.Models
{
    public class RegisterModel
    {

        [Required(ErrorMessage = "Не указан Email")]
        public string name { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string passw { get; set; }

        [DataType(DataType.Password)]
        [Compare("passw", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }
    }
}
