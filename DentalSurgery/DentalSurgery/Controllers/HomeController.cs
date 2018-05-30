using DentalSurgery.BLL;
using DentalSurgery.Models;
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
        public HomeController()
        {
            _context = new DentalBaseContext();
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

        [Authorize]
        public ActionResult WriteOpinion()
        {
            return View();
        }
    }
}