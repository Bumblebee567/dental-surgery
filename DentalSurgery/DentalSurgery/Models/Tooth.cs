using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DentalSurgery.Models
{
    public class Tooth
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public virtual ICollection<Surgery> Surgeries { get; set; }
    }
}