using DentalSurgery.BLL;
using DentalSurgery.Models;
using DentalSurgery.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DentalSurgery.Utiles
{
    public class AppointmentDropdownGenerator
    {
        public static MakeAppointmentViewModel GenerateAppointmentViewModelDropdowns(DentalBaseContext context)
        {
            var model = new MakeAppointmentViewModel();
            model.Surgeries.AddRange(context.Set<Surgery>());
            model.Teeth.AddRange(context.Set<Tooth>());
            model.Patients.AddRange(context.Set<AppUser>());
            foreach (var item in model.Surgeries)
            {
                model.SurgeryChoice.Add(new SelectListItem { Text = item.Name, Value = item.SurgeryId.ToString() });
            }
            foreach (var item in model.Teeth)
            {
                model.ToothChoice.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            foreach (var item in model.Patients)
            {
                model.PatientChoice.Add(new SelectListItem { Text = item.Email, Value = item.Id.ToString() });
            }
            return model;
        }
    }
}