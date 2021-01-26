using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppNov14.Models
{
    public class Users : IdentityUser
    {
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string FamilyName { get; set; }      
    }
    
}
    
