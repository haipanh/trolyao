using Solar.Web.Mvc;
using System.Web.Mvc;

namespace Application.Website.Controllers
{
    public class AdminController : ApplicationController
    {
        private AuthenticationService _authenticationService = new AuthenticationService();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string userName, string password)
        {
            if (_authenticationService.Authenticate(userName, password, true))
            {
                return this.RedirectToAction(c => c.Index());
            }

            return View();
        }

        public ActionResult LogOut()
        {
            _authenticationService.SignOut();

            return this.RedirectToAction(c => c.Login());
        }

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}
