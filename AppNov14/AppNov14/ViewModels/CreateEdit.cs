using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppNov14.ViewModels
{
    public class CreateUserViewModel
    {
        public string LoginApp { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Password { get; set; }
       
    }
    public class EditUserViewModel
    {
        public string Id { get; set; }
        public string LoginApp { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
    }
}
