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

            VotesAjaxResponseModel responseModel;

            if(!User.Identity.IsAuthenticated)
            {
                responseModel = new VotesAjaxResponseModel()
                {
                    Id = id,
                };

                return Json(responseModel, JsonRequestBehavior.AllowGet);
            }

            var model = new CommentVoteModel();

            var (message, points) = await model.Upvote(id, User.Identity.Name);

            responseModel = new VotesAjaxResponseModel()
            {
                Id = id,
                Points = points,
                Status = message
            };
            return Json(responseModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> Downvote(int id)
        {
            VotesAjaxResponseModel responseModel;

            if (!User.Identity.IsAuthenticated)
            {
                responseModel = new VotesAjaxResponseModel()
                {
                    Id = id,
                };

                return Json(responseModel, JsonRequestBehavior.AllowGet);
            }

            var model = new CommentVoteModel();
            var (message, points) = await model.Downvote(id, User.Identity.Name);

            responseModel = new VotesAjaxResponseModel()
            {
                Id = id,
                Points = points,
                Status = message
            };

            return Json(responseModel, JsonRequestBehavior.AllowGet);
        }
    }
}