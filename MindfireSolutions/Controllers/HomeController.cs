using MindfireSolutions.DataAccess;
using MindfireSolutions.Service.ServiceClass;
using MindfireSolutions.Service.ServiceInterface;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace MindfireSolutions.Controllers
{
    public class HomeController : Controller
    {
        private readonly IIndex _index;
        public HomeController(IIndex index)
        {
            _index = index;
        }
        private DAL reference = new DAL();
        [AllowAnonymous]
        public ActionResult Index()
        {
            var data = _index.Fetch();
            return View(data);
        }
        public ActionResult DynamicMenu()
        {
            var _blogTopic = reference.BlogTopics.ToList();
            return PartialView("DynamicMenu", _blogTopic);
        }
    }
}