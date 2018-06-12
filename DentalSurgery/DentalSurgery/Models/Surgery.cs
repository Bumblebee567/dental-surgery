using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DentalSurgery.Models
{
    public class Surgery
    {
        public Guid SurgeryId { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public double Price { get; set; }
        public double EstimatedTime { get; set; }
        public bool HasTooth { get; set; }
        public virtual ICollection<Visit> Visits { get; set; }
        public virtual Tooth Tooth { get; set; }
    }
}