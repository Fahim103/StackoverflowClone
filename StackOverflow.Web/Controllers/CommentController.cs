using StackOverflow.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace StackOverflow.Web.Controllers
{
    public class CommentController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddComment(AddCommentModel model)
        {
            if (ModelState.IsValid)
            {
                await model.AddComment(User.Identity.Name);
                return RedirectToAction("Details", "Post", new { id = model.PostId });
            }

            return View(model);
        }
    }
}