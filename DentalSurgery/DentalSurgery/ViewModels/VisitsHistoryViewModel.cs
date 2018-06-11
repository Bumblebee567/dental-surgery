using DentalSurgery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DentalSurgery.ViewModels
{
    public class VisitsHistoryViewModel
    {
        public DateTime VisitDate { get; set; }
        public List<Surgery> Surgeries { get; set; } = new List<Surgery>();
        public double TotalCost { get; set; }
        public double TotalTime { get; set; }
    }
}