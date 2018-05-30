using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DentalSurgery.Models
{
    public class AppUser : IdentityUser
    {
        public Guid UserId { get; set; } = Guid.NewGuid();
        public virtual ICollection<Visit> Visits { get; set; }
        public virtual ICollection<Opinion> Opinions { get; set; }
    }
}