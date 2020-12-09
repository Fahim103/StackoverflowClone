using StackOverflow.Core;
using StackOverflow.Core.Exceptions;
using StackOverflow.Web.Models;
using System.Threading.Tasks;
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

            try
            {
                var model = new PostDetailsModel();
                model.GetModelById(id);

                return View(model);
            }
            catch (EntityNotFoundException)
            {
                return RedirectToAction("Index");
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Upvote(int id)
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

            var model = new PostVoteModel();
            var (message, points) = await model.Upvote(id, User.Identity.Name);

            responseModel = new VotesAjaxResponseModel()
            {
                Id = id,
                Points = points,
                Status = message
            };

            return Json(responseModel, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
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

            var model = new PostVoteModel();
            var (message, points) = await model.Downvote(id, User.Identity.Name);

            responseModel = new VotesAjaxResponseModel()
            {
                Id = id,
                Points = points,
                Status = message
            };

            return Json(responseModel, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AcceptAnswer(PostAnswerAcceptModel model)
        {
            if(ModelState.IsValid)
            {
                model.AcceptAnswer();
            }
            
            return RedirectToAction("Details", "Post", new { id = model.PostId });
        }

        [Authorize(Roles = StringConstants.ADMIN_ROLE)]
        [HttpGet]
        public ActionResult ManagePosts()
        {
            var model = new PostModel();
            model.LoadModelData(true);

            return View(model.Posts);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = StringConstants.ADMIN_ROLE)]
        public ActionResult MarkDuplicate(int postId)
        {
            var model = new PostManageModel();
            model.MarkPostDuplicate(postId);

            return RedirectToAction("ManagePosts");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = StringConstants.ADMIN_ROLE)]
        public ActionResult Hide(int postId)
        {
            var model = new PostManageModel();
            model.HidePost(postId);

            return RedirectToAction("ManagePosts");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = StringConstants.ADMIN_ROLE)]
        public ActionResult RemoveMarkDuplicate(int postId)
        {
            var model = new PostManageModel();
            model.RemoveMarkDuplicate(postId);

            return RedirectToAction("ManagePosts");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = StringConstants.ADMIN_ROLE)]
        public ActionResult Show(int postId)
        {
            var model = new PostManageModel();
            model.ShowPost(postId);

            return RedirectToAction("ManagePosts");
        }
    }
}