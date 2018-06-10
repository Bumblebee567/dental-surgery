using DentalSurgery.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DentalSurgery.ViewModels
{
    public class MakeAppointmentViewModel
    {
        [Required]
        public DateTime? Date { get; set; }
        public List<Guid> SurgeriesIDs { get; set; } = new List<Guid>();
        public List<Guid> TeethIDs { get; set; } = new List<Guid>();
        public List<Surgery> Surgeries { get; set; } = new List<Surgery>();
        public List<Tooth> Teeth { get; set; } = new List<Tooth>();
        public List<SelectListItem> SurgeryChoice { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> ToothChoice { get; set; } = new List<SelectListItem>();
        public int NumberOfSurgeries { get; set; }
    }
}