using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DentalSurgery.Models
{
    public class Surgery
    {
        public Guid SurgeryId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public virtual ICollection<Visit> Visits { get; set; }
    }
}