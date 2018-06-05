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
        public Guid FirstSurgeryId { get; set; }
        public string FirstSurgeryTooth { get; set; }
        public Guid SecondSurgeryId { get; set; }
        public string SecondSurgeryTooth { get; set; }
        public Guid ThirdSurgeryId { get; set; }
        public string ThirdSurgeryTooth { get; set; }
        public List<Surgery> Surgeries { get; set; } = new List<Surgery>();
        public IEnumerable<string> Teeth { get; set; } = new List<string>();
        public List<SelectListItem> SurgeryChoice { get; set; } = new List<SelectListItem>();
    }
}