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
    public static class AppointmentDropdownGenerator
    {
        private static IEnumerable<TSource> DistinctBy<TSource, TKey> (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
        public static MakeAppointmentViewModel GenerateAppointmentViewModelDropdowns(DentalBaseContext context)
        {
            var model = new MakeAppointmentViewModel();
            var surgeries = context.Surgeries.DistinctBy(x => x.Name);
            model.Surgeries.AddRange(surgeries);
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
                if (item.FirstName != null && item.LastName != null)
                {
                    var text = $"{item.FirstName} {item.LastName}";
                    model.PatientChoice.Add(new SelectListItem { Text = text, Value = item.Id.ToString() });
                }
            }
            return model;
        }
    }
}