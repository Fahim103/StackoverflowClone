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
            var model = new PostModel();
            model.LoadModelData();

            return View(model.Posts);
        }

        [Authorize]
        public ActionResult Create()
        {
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

        [HttpGet]
        public ActionResult Details(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index");
            }

            var model = new PostDetailsModel();
            model.GetModelById(id);

            return View(model);
        }
    }
}