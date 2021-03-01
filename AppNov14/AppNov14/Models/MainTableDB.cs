using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AppNov14.Models
{
    public class MainTableDB
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(135, 136, ErrorMessage = "Указан не правильный цех")]
        public int Gild { get; set; }

        [Required]
        public string TypeOfMaterial { get; set; }

        [Required]
        public string NameOfTypeMaterial { get; set; }

        [DisplayFormat(DataFormatString = "{0,000}", ApplyFormatInEditMode = true)]
        [Required]
        public decimal Quantity { get; set; }

        public decimal Leftovers { get; set; }

        [Required]
        public string Provider { get; set; }

        [Required]
        public string Manufacturer { get; set; }

        [Required]
        public string Document { get; set; }

        [Required]
        public string NumberOfDocument { get; set; }

        [Required]
        public string Indexation { get; set; }

        [Required]
        public string Line { get; set; }

        [Required]
        public DateTime DocDate { get; set; }

        public string Employee { get; set; }

        public string IpAdress { get; set; }

        public DateTime AutoDate { get; set; }

        public string Remarks { get; set; }

        public int OperationType { get; set; }

        public List<SelectListItem> listyNameType { get; set; }
        public List<SelectListItem> listyProvider { get; set; }
        public List<SelectListItem> listyManufacturer { get; set; }
        public List<SelectListItem> listyLine { get; set; }
        public List<SelectListItem> listyParties { get; set; }
        public List<SelectListItem> listyPartiesNames { get; set; }
    }

    public class MainTableDBIncoming
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(135, 136, ErrorMessage = "Указан не правильный цех")]
        public int Gild { get; set; }

        [Required]
        public string TypeOfMaterial { get; set; }

        [Required]
        public string NameOfTypeMaterial { get; set; }

        [DisplayFormat(DataFormatString = "{0,000}", ApplyFormatInEditMode = true)]
        [Required]
        public float Quantity { get; set; }

        [Required]
        public string Provider { get; set; }

        [Required]
        public string Manufacturer { get; set; }

        [Required]
        public string NumberOfDocument { get; set; }

        [Required]
        public string Indexation { get; set; }

        [Required]
        public DateTime DocDate { get; set; }

        public string Remarks { get; set; }

    }

    public class InnerModelToPost
    {

        [Range(0, int.MaxValue, ErrorMessage = "Число не может быть меньше 0")]
        [Required]
        public int NumberOfRecords { get; set; }

        [Required]
        public DateTime DateStart { get; set; }

        [Required]
        public DateTime DateFinish { get; set; }

        public DataTable listil { get; set; }

        public int Count { get; set; }

    }
    public class MainTableParties
    {

        public string Document { get; set; }

        public string NumberOfDocument { get; set; }

        public DataTable listil { get; set; }
        public List<SelectListItem> listyParties { get; set; }
    }

    public class MainTableIndex
    {
        [Required]
        public string TypeOfMaterial { get; set; }

        [Required]
        public string NameOfTypeMaterial { get; set; }

        [Required]
        public string Provider { get; set; }

        [Required]
        public string Manufacturer { get; set; }

        public DataTable listil { get; set; }
    }
}