using DentalSurgery.BLL;
using DentalSurgery.Models;
using DentalSurgery.Services;
using DentalSurgery.Utiles;
using DentalSurgery.ViewModels;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DentalSurgery.Controllers
{
    public class HomeController : Controller
    {
        private UserManager<AppUser> _userManager;
        private DentalBaseContext _context;
        private OpinionManager _opinionManager;
        private IEnumerable<Opinion> _opinions;
        public HomeController()
        {
            _context = new DentalBaseContext();
            _opinionManager = new OpinionManager(_context);
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var password = DefaultPasswordsGenerator.GenerateDefaultPassword(8);
                var user = new AppUser()
                {
                    Email = model.Email,
                    PasswordHash = password,
                    UserName = model.FirstName + model.LastName,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };
                if (_userManager == null)
                {
                    _userManager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
                }
                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    EmailSender.SendDefaultPassword(user.Email, password);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            _userManager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            var authManager = HttpContext.GetOwinContext().Authentication;

            AppUser user = _userManager.FindByEmail(model.Email);
            if (user != null)
            {
                var ident = _userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                authManager.SignIn(new AuthenticationProperties { IsPersistent = false }, ident);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [Authorize]
        public ActionResult Logout()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ActionResult WriteOpinion()
        {
            return PartialView("WriteOpinion");
        }
        [HttpPost]
        public ActionResult WriteOpinion(OpinionViewModel opinion)
        {
            var userId = User.Identity.GetUserId();
            var author = _context.Users.Where(x => x.Id == userId).FirstOrDefault();
            opinion.Author = author;
            _opinionManager.AddOpinion(opinion);
            return RedirectToAction("Opinions", "Home");

        }
        [HttpGet]
        public ActionResult Opinions()
        {
            _opinions = _opinionManager.GetAllOpinions().OrderByDescending(x => x.Date);
            return View(_opinions);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult MakeAppointment(int numberOfSurgeries = 0, DateTime? visitDate = null)
        {
            var model = AppointmentDropdownGenerator.GenerateAppointmentViewModelDropdowns(_context);
            model.NumberOfSurgeries = numberOfSurgeries;
            model.Date = visitDate;
            return View(model);
        }
        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Save")]
        public ActionResult Save(MakeAppointmentViewModel model)
        {
            var visit = new Visit();
            visit.Date = model.Date.Value;
            visit.Patient = _context.Set<AppUser>().Where(x => x.Id == model.PatientID.ToString()).FirstOrDefault();
            for (int i = 0; i < model.SurgeriesIDs.Count; i++)
            {
                var surgeryID = model.SurgeriesIDs[i];
                var toothID = model.TeethIDs[i];
                var name = _context.Set<Surgery>().Where(x => x.SurgeryId == surgeryID).FirstOrDefault().Name;
                var estimatedTime = _context.Set<Surgery>().Where(x => x.SurgeryId == surgeryID).FirstOrDefault().EstimatedTime;
                var price = _context.Set<Surgery>().Where(x => x.SurgeryId == surgeryID).FirstOrDefault().Price;
                var tooth = _context.Set<Tooth>().Where(x => x.Id == toothID).FirstOrDefault();
                if(visit.Surgeries == null)
                    visit.Surgeries = new List<Surgery>();
                visit.Surgeries.Add(new Surgery
                {
                    Name = name,
                    EstimatedTime = estimatedTime,
                    Price = price,
                    Tooth = tooth
                });
            }
            _context.Visits.Add(visit);
            _context.Surgeries.AddRange(visit.Surgeries);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Number")]
        public ActionResult Number(MakeAppointmentViewModel model)
        {
            return RedirectToAction("MakeAppointment", "Home", new { numberOfSurgeries = model.NumberOfSurgeries, visitDate = model.Date});
        }
    }
}