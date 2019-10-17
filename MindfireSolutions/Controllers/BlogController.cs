using MindfireSolutions.DataAccess;
using System.Web.Mvc;
using System.Web.Security;
using System.Linq;
using MindfireSolutions.ViewModel;
using MindfireSolutions.Models;
using MindfireSolutions.Service.ServiceInterface;
using MindfireSolutions.Service.ServiceClass;
using MindfireSolutions.DataModel;

namespace MindfireSolutions.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        /// <summary>
        /// Reference fot Blogmanager
        /// </summary>
        private readonly IBlogManager _blogManager;

        /// <summary>
        /// Constructor of BlogConrtoller
        /// </summary>
        /// <param name="blogManager"></param>
        public BlogController(IBlogManager blogManager)
        {
            _blogManager = blogManager;
        }

        /// <summary>
        /// Method for View of Blog Creation page with Blog Creation Fields.
        /// </summary>
        /// <returns>View for Blog Creation.</returns>
        [HttpGet]
        public ActionResult PostBlog()
        {
            var email = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
            var data = _blogManager.RenderBlog(email);
            return View(data);
        }



        /// <summary>
        /// Method For adding Blog To the Database
        /// </summary>
        /// <param name="blog"></param>
        /// <returns>Blog View to Add more Blogs.</returns>
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult PostBlog(VMBlog blog)
        {
            var blogCreated = _blogManager.Create(blog);
            if (blogCreated)
            {
                return RedirectToAction("Dashboard", "Author");
            }
            return RedirectToAction("PostBlog", "Blog");
        }

        /// <summary>
        /// Method to Render Post on Clicking 0f Submenu on Index page
        /// </summary>
        /// <param name="topicId"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult RenderPost(int topicId)
        {
            var blogResult = _blogManager.RenderPost(topicId);
            if (blogResult != null)
            {
                return View(blogResult);
            }
            return View();
        }


        public ActionResult BlogTopic()
        {
            var data = _blogManager.Topics();
            return PartialView("_BlogTopic", data);
        }

        [HttpPost]
        public JsonResult GetMembers(int topicId)
        {
            var data = _blogManager.SubTopics(topicId);
            return Json(data);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddTopic(AddTopic topic)
        {
            var topicAdded = _blogManager.AddTopic(topic);
            return Json(new { success = topicAdded }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="topicId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult FetchTags(int topicId)
        {
            var data = _blogManager.FetchTags(topicId);
            if (data != null)
            {
                return Json(data);
            }
            return Json(null);
        }

        /// <summary>
        /// This function gets called from Render post view to get details of blog clicked
        /// </summary>
        /// <param name="blogId"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult GetBlogDescription(int blogId)
        {
            var blogDescription = _blogManager.FetchBlogDescription(blogId);
            return View(blogDescription);
        }

        /// <summary>
        /// This is to save comments on blogs by authenticated user only
        /// </summary>
        /// <param name="details"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveBlogComments(VMSaveBlogComments details)
        {
            if (details != null)
            {
                string email = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
                _blogManager.SaveBlogsComments(details, email);
            }
            return RedirectToAction("GetBlogDescription", "Blog", new { details.BlogId });
        }

        /// <summary>
        /// This is to Delete comments on blogs by User whose had commented
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        public JsonResult DeleteVisitorComments(int commentId, string email)
        {
            var userEmail = (FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name);
            if (email == userEmail)
            {
                if (_blogManager.DeleteBlogComments(commentId, userEmail))
                {
                    return Json(new { success = true, }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { success = false, }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false, }, JsonRequestBehavior.AllowGet);

        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Search(string SearchTag)
        {
            var data = _blogManager.Search(SearchTag);
            ViewBag.Message = SearchTag;
            return View(data);
        }

        [HttpPost]
        public ActionResult Like(int blogId)
        {
            string email = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
            var flag = _blogManager.Likes(email, blogId);
            if (flag)
            {
                return Json(true);
            }
            return Json(false);

        }
        [HttpPost]
        public ActionResult Dislike(int blogId)
        {
            string email = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
            var flag = _blogManager.DisLike(email, blogId);
            if (flag)
            {
                return Json(true);
            }
            return Json(false);

        }
        [HttpPost]
        public ActionResult Spam(int blogId)
        {
            string email = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
            var flag = _blogManager.Spam(email, blogId);
            if (flag)
            {
                return Json(true);
            }
            return Json(false);

        }

    }
}