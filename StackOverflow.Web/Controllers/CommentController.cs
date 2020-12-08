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

        [HttpPost]
        public async Task<ActionResult> Upvote(int id)
        {
            var model = new CommentVoteModel();
            var data = await model.Upvote(id, User.Identity.Name);
            dynamic returnData = new
            {
                id,
                point = data.Item2
            };
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> Downvote(int id)
        {
            var model = new CommentVoteModel();
            var data = await model.Downvote(id, User.Identity.Name);
            dynamic returnData = new
            {
                id,
                point = data.Item2
            };
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }
    }
}