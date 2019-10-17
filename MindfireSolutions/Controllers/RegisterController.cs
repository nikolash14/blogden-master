using MindfireSolutions.DataAccess;
using MindfireSolutions.Service.ServiceClass;
using MindfireSolutions.Service.ServiceInterface;
using MindfireSolutions.ViewModel;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MindfireSolutions.Controllers
{
    [Authorize]
    public class RegisterController : Controller
    {
        /// <summary>
        /// Reference for Dependency injection.
        /// </summary>
        private IManageUser user;

        /// <summary>
        /// Constructor of Registration Controller.
        /// </summary>
        /// <param name="_user"></param>
        public RegisterController(IManageUser _user)
        {
            this.user = _user;
        }

        /// <summary>
        /// User Registation page if user is not logged in.
        /// </summary>
        /// <returns>View for Registration if the user is not logged in id the user is logged in it takes user to Index.cshtml page.</returns>
        [AllowAnonymous]
        public ActionResult UserRegistration()
        {
            if (User.Identity.IsAuthenticated)
            {
                string email = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        /// <summary>
        /// Registration of the user with user Credentials for Login.
        /// </summary>
        /// <param name="userDetails"></param>
        /// <returns>View After Loggin in the User by Creating Cookie for the User.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult UserRegistration(VMUser userDetails)
        {
            if (ModelState.IsValid)
            {
                var email = user.Create(userDetails);
                if (email != string.Empty)
                {
                    FormsAuthentication.SetAuthCookie(email, false);
                    return RedirectToAction("Index", "Home");
                }
                return View();
            }
            return View();
        }
        /// <summary>
        /// Check The availability of Email while SignUp
        /// </summary>
        /// <param name="email"></param>
        /// <returns>JSON true or false</returns>

        [AllowAnonymous]
        public JsonResult EmailAvailability(string email)
        {
            var localEmail = new DAL().Users.Where(m => m.Email == email).FirstOrDefault();
            return localEmail != null ? Json(0) : Json(1);
        }

        /// <summary>
        /// Logout User
        /// </summary>
        /// <returns>End Session for the User redirects to index page.</returns>
        public ActionResult UserLogout()
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            FormsAuthentication.SignOut();
            this.Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            this.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            this.Response.Cache.SetNoStore();
            return RedirectToAction("Index", "Home");
        }
    }
}