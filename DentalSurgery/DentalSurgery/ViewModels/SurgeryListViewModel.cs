using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DentalSurgery.ViewModels
{
    public class SurgeryListViewModel
    {
        public string name;
        public SurgeryListViewModel(string name)
        {
            this.name = name.First().ToString().ToUpper() + name.Substring(1);
        }
    }
}