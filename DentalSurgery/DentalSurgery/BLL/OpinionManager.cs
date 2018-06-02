using DentalSurgery.Models;
using DentalSurgery.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DentalSurgery.BLL
{
    public class OpinionManager
    {
        private static DentalBaseContext _context;
        public static void AddOpinion(OpinionViewModel model)
        {
            _context = new DentalBaseContext();
            _context.Opinions.Add(new Models.Opinion
            {
                Date = model.PubDate,
                Content = model.Content,
                Author = model.Author
            });
            _context.SaveChanges();
        }
        public static void DeleteOpinion(OpinionViewModel model)
        {

        }
        public static IQueryable<Opinion> GetAllOpinions()
        {
            _context = new DentalBaseContext();
            return _context.Set<Opinion>();
        }
    }
}