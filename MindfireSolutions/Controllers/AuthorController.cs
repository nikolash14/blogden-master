using MindfireSolutions.Service.ServiceInterface;
using MindfireSolutions.ViewModel;
using System.Web.Mvc;
using System.Web.Security;

namespace MindfireSolutions.Controllers
{
    [Authorize]
    public class AuthorController : Controller
    {
        /// <summary>
        /// Interface for Reference of Author
        /// </summary>
        private readonly IAuthor _author;





        /// <summary>
        /// Author Controller creates the object for the author and add reference to the interface.
        /// </summary>
        /// <param name="author"></param>
        public AuthorController(IAuthor author)
        {
            _author = author;
        }




        /// <summary>
        /// Action method gives the Author dashBoard Data
        /// </summary>
        /// <returns>Dashboard object with View()</returns>
        public ActionResult Dashboard()
        {
            string email = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
            var author = _author.Data(email);
            return View(author);
        }




        /// <summary>
        /// Action method gives the Profile of the Author/User
        /// </summary>
        /// <returns>view with profile object</returns>
        public ActionResult AuthorProfile()
        {
            string email = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
            var author = _author.Details(email);
            return View(author);
        }




        /// <summary>
        /// Method for Giving View to the User for Editing Profile
        /// </summary>
        /// <param name="email"></param>
        /// <returns>View with User Details object</returns>
        [HttpGet]
        public ActionResult EditProfile()
        {
            string email = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
            var author = _author.Details(email);
            return View(author);
        }




        /// <summary>
        /// Method Edits the User Informations
        /// </summary>
        /// <param name="author"></param>
        /// <returns>View To Profile/Same Edit Page</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(VMAuthor author)
        {
            if (ModelState.IsValid)
            {
                var result = _author.Edit(author);
                if (result)
                {
                    return RedirectToAction("AuthorProfile", "Author");
                }
            }
            TempData["Message"] = "Fill all the Data Correctly !";
            return RedirectToAction("EditProfile", "Author");
        }




        /// <summary>
        /// Delete the Blogger Details with his/her all blogs
        /// </summary>
        /// <returns>Index page</returns>
        public ActionResult Delete()
        {
            string email = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
            var result = _author.Delete(email);
            if (result)
            {
                return RedirectToAction("UserLogout", "Register");
            }
            else
            {
                return View("AutherProfile", "Blog");
            }
        }



        /// <summary>
        /// Action Method gives all blogs written by the Blogger
        /// </summary>
        /// <returns></returns>
        public ActionResult AllBlogs()
        {
            string email = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
            var data = _author.AllBlogs(email);
            return View(data);
        }




        /// <summary>
        /// Delete a Blog
        /// </summary>
        /// <param name="blogId"></param>
        /// <returns>To All blogs by Blogger page</returns>
        public ActionResult DeleteBlog(int blogId)
        {
            var flag = _author.DeleteBlog(blogId);
            return RedirectToAction("AllBlogs", "Author");
        }




        /// <summary>
        /// Method for Display Edit View
        /// </summary>
        /// <param name="blogId"></param>
        /// <returns>Edit View page with object of VMEditBlog</returns>
        [HttpGet]
        public ActionResult EditBlog(int blogId)
        {
            var data = _author.EditBlog(blogId);
            return View(data);
        }




        /// <summary>
        /// Method for Holding the Form data while Posting From EditBlog This Accepts the HTML data to the database
        /// </summary>
        /// <param name="blog"></param>
        /// <returns>Redirects to the Edit Page with the Same Edited BlogId</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult EditBlog(VMEditBlog blog)
        {
            if (ModelState.IsValid)
            {
                var flag = _author.SaveEditedBlog(blog);
                if (flag)
                {
                    return RedirectToAction("EditBlog", "Author", new { blog.BlogId });
                }
            }

            TempData["Message"] = "Error in Editing the Data 'Try Again'";
            return RedirectToAction("EditBlog", "Author", new { blog.BlogId });
        }




        [HttpGet]
        public ActionResult PersonalDetails()
        {
            string email = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
            var author = _author.PersonalDetails(email);
            return View(author);
        }




        [HttpPost]
        public ActionResult PersonalDetails(VMPersonalDetails data)
        {
            var flag = _author.SavePersonalData(data);
            if (flag)
            {
                return RedirectToAction("AuthorProfile", "Author");
            }
            return View();
        }




        /// <summary>
        /// Hide a Blog Describtion by Archiving the Blog
        /// </summary>
        /// <param name="blogId"></param>
        /// <returns>All Blogs Page</returns>
        [HttpGet]
        public ActionResult ArchiveBlog(int blogId)
        {
            var flag = _author.ArchiveBlog(blogId);
            if (flag)
            {
                return RedirectToAction("Dashboard", "Author");
            }
            ViewBag.Message = "Error in Archiving Data";
            return RedirectToAction("AllBlogs", "Author");

        }




        /// <summary>
        /// Extract Archived Blogs
        /// </summary>
        /// <param name="blogId"></param>
        /// <returns>All Blogs Page</returns>
        [HttpGet]
        public ActionResult ExtractBlog(int blogId)
        {
            var flag = _author.Extract(blogId);
            if (flag)
            {
                return RedirectToAction("Dashboard", "Author");
            }
            ViewBag.Message = "Error in Extracting Data";
            return RedirectToAction("AllBlogs", "Author");

        }




        /// <summary>
        /// Action Method for Quick Draft
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DraftBlog(VMDraftBlog data)
        {
            if (ModelState.IsValid)
            {
                var flag = _author.Draft(data);
                if (flag)
                {
                    return RedirectToAction("Dashboard", "Author");
                }
            }

            TempData["Message"] = "Error, Please Fill all the input fields for Drafting the Blog !";
            return RedirectToAction("Dashboard", "Author");
        }


        [HttpGet]
        public ActionResult AllArchivedBlog()
        {
            string email = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
            var data = _author.AllArchivedBlog(email);
            return View(data);
        }


        [HttpGet]
        public ActionResult AllDraftedContent()
        {
            string email = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
            var data = _author.AllDraftedContent(email);
            return View(data);
        }


        public ActionResult Chart()
        {
            string email = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
            var data = _author.AllDraftedContent(email);
            return View(data);
        }

        [HttpPost]
        public JsonResult ChartData()
        {
            string email = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
            var author = _author.Data(email);
            return Json(author);
        }
    }
}