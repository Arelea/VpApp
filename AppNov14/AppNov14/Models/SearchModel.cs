using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AppNov14.Models
{
    public class SearchModel
    {    

        public string TypeOfMaterial { get; set; }

        public string NameOfTypeMaterial { get; set; }

        public string Provider { get; set; }

        public string Manufacturer { get; set; }

        public string Document { get; set; }

        public string NumberOfDocument { get; set; }

        public string Indexation { get; set; }

        public string Line { get; set; }

        public string Employee { get; set; }

        public DataTable listil { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateFinish { get; set; }
        
        public List<SelectListItem> listyType { get; set; }
        public List<SelectListItem> listyNameType { get; set; }
        public List<SelectListItem> listyProvider { get; set; }
        public List<SelectListItem> listyManufacturer { get; set; }
        public List<SelectListItem> listyParties { get; set; }
        public List<SelectListItem> listyPartiesNames { get; set; }
        public List<SelectListItem> listyLine { get; set; }
    }
}
