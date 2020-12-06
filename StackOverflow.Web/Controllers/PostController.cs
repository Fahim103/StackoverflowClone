using StackOverflow.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace StackOverflow.Web.Controllers
{
    public class PostController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Create()
        {
            var model = new CreatePostModel();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreatePostModel createPostModel)
        {
            if (ModelState.IsValid)
            {
                await createPostModel.AddPost(User.Identity.Name);
                return RedirectToAction("Index");
            }

            return View(createPostModel);
        }
    }
}