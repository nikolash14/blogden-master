using MindfireSolutions.Service.ServiceClass;
using MindfireSolutions.Service.ServiceInterface;
using MindfireSolutions.ViewModel;
using System.Web.Mvc;

namespace MindfireSolutions.Controllers
{
    public class ContactController : Controller
    {
        /// <summary>
        /// Reference for Dependency Injection.
        /// </summary>
        private IContactMessage message;

        /// <summary>
        /// Constructor for ContactController to Create object by Dependency Injection.
        /// </summary>
        /// <param name="_message"></param>
        public ContactController(IContactMessage _message)
        {
            this.message = _message;
        }


        /// <summary>
        /// Method for Accepting Messages by User.
        /// </summary>
        /// <param name="visitorData"></param>
        /// <returns>JSON</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult VisitorComment(VMMessage visitorData)
        {
            if (ModelState.IsValid)
            {
                if (message.CreateMessage(visitorData))
                {
                    return Json(new { success = true, }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { success = false, }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult BloggerDetails(int id)
        {
            var data = message.GetBloggerDetails(id);
            return View(data);
        }
    }
}