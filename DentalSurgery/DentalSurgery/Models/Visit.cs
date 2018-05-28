using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DentalSurgery.Models
{
    public class Visit
    {
        public Guid VisitId { get; set; } = Guid.NewGuid();
        public DateTime Date { get; set; }
        public AppUser Patient { get; set; }
        public virtual ICollection<Surgery> Surgeries { get; set; }
    }
}