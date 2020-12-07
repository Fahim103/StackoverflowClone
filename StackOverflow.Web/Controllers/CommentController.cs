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
        public ActionResult Upvote(int id)
        {
            // TODO call server and downvote
            try
            {
                dynamic returnData = new
                {
                    id,
                    point = 10
                };
                return Json(returnData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                dynamic returnData = new
                {
                    id,
                    error = "Failed to Upvote",
                };
                return Json(returnData, JsonRequestBehavior.AllowGet);
            }
            
        }

        [HttpPost]
        public ActionResult Downvote(int id)
        {
            // TODO call server and downvote
            dynamic returnData = new
            {
                id,
                point = 0
            };
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }
    }
}