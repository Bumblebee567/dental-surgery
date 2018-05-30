using DentalSurgery.Models;
using System;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DentalSurgery.ViewModels
{
    public class OpinionViewModel
    {
        public DateTime PubDate { get; set; } = DateTime.Now;
        public AppUser Author { get; set; } = HttpContext.Current.User as AppUser;
        public string Content { get; set; }
    }
}