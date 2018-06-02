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
        public OpinionManager()
        {
        }
        public OpinionManager(DentalBaseContext context)
        {
            _context = context;
        }
        public void AddOpinion(OpinionViewModel model)
        {
            var opinion = new Opinion
            {
                Date = model.PubDate,
                Content = model.Content,
                Author = model.Author
            };
            _context.Opinions.Add(opinion);
            _context.SaveChanges();
        }
        public static void DeleteOpinion(OpinionViewModel model)
        {

        }
        public List<Opinion> GetAllOpinions()
        {
            return _context.Opinions.ToList();
        }
    }
}