using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AppNov14.Models
{
    public class Warehouse135Model
    {
        [Key]
        public int Id { get; set; }
             
        public string TypeOfMaterial { get; set; }
       
        public string NameOfTypeMaterial { get; set; }      
     
        public string Provider { get; set; }

        public string Manufacturer { get; set; }

        public string[] list { get; set; }

        public List<DataTable> listil { get; set; }
    }
}
