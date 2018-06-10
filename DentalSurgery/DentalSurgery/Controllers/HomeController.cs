using DentalSurgery.BLL;
using DentalSurgery.Models;
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
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    PasswordHash = model.Password
                };
                if (_userManager == null)
                {
                    _userManager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
                }
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
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
        [Authorize]
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
            return null;
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Number")]
        public ActionResult Number(MakeAppointmentViewModel model)
        {
            return RedirectToAction("MakeAppointment", "Home", new { numberOfSurgeries = model.NumberOfSurgeries, visitDate = model.Date});
        }
    }
}