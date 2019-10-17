using System.Web.Mvc;

namespace AssignmentMVC.Controllers
{
    public class ErrorHandlerController : Controller
    {
        /// <summary>
        /// Method for handling 500 errors.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Method for handling 404 errors.
        /// </summary>
        /// <returns></returns>
        public ActionResult NotFound()
        {
            return View();
        }
    }
}