using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace AppNov14.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Логин")]
        public string LoginApp { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
 
        public string ReturnUrl { get; set; }

    }
}
