using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DentalSurgery.Models
{
    public class Opinion
    {
        public Guid OpinionId { get; set; } = Guid.NewGuid();
        public AppUser Author { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
    }
}