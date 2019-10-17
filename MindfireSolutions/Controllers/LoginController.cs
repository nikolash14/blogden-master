using MindfireSolutions.Custom;
using MindfireSolutions.DataAccess;
using MindfireSolutions.ViewModel;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace MindfireSolutions.Controllers
{
    public class LoginController : Controller
    {
        /// <summary>
        /// Reference for dependency Injection.
        /// </summary>
        DAL dbReference = new DAL();
        private ICustomHelper helper;
        /// <summary>
        /// Constructor of Login Controller to create Object by Dependency Injection.
        /// </summary>
        /// <param name="_helper"></param>
        public LoginController(ICustomHelper _helper)
        {
            this.helper = _helper;
        }
        /// <summary>
        /// Method for log in The User by Fetching data from Login modal.
        /// </summary>
        /// <param name="userData"></param>
        /// <returns>JSON to shoe the user is Authenticated.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult UserLogin(VMUserLogin userData)
        {
            if (ModelState.IsValid)
            {
                string localPassword = helper.HashValue(userData.PasswordValue);
                var data = dbReference.Users.FirstOrDefault(m => m.Email == userData.EmailId && m.Password.Equals(localPassword));
                if (data != null)
                {
                    FormsAuthentication.SetAuthCookie(userData.EmailId, false);
                    return Json(new { success = true, }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { success = false, }, JsonRequestBehavior.AllowGet);
            }
            return View();
        }
    }
}