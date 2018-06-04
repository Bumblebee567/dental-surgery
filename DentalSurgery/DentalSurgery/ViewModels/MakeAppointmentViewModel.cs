using DentalSurgery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DentalSurgery.ViewModels
{
    public class MakeAppointmentViewModel
    {
        public DateTime Date { get; set; }
        public string FirstSurgeryName { get; set; }
        public string FirstSurgeryTooth { get; set; }
        public string SecondSurgeryName { get; set; }
        public string SecondSurgeryTooth { get; set; }
        public string ThirdSurgeryName { get; set; }
        public string ThirdSurgeryTooth { get; set; }
    }
}