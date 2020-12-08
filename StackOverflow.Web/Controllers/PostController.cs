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

            var model = new PostDetailsModel();
            model.GetModelById(id);

            return View(model);
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
    }
}