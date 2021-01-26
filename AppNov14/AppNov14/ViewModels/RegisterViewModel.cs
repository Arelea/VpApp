using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppNov14.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Логин")]
        public string LoginApp { get; set; }

        [Required]
        [Display(Name = "Имя")]
        public string FName { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        public string LName { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

       

       
    }
}
