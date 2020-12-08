using Autofac;
using Microsoft.AspNet.Identity.Owin;
using StackOverflow.Core.Entities;
using StackOverflow.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace StackOverflow.Web.Models
{
    public class PostVoteModel
    {
        private readonly IPostPointService _postPointService;
        private readonly IPostService _postService;
        private readonly ApplicationUserManager _userManager;

        public PostVoteModel()
        {
            _postPointService = Startup.AutofacContainer.Resolve<IPostPointService>();
            _postService = Startup.AutofacContainer.Resolve<IPostService>();
            _userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
        }

        public PostVoteModel(IPostPointService postPointService, IPostService postService)
        {
            _postPointService = postPointService;
            _postService = postService;
            _userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
        }

        public async Task<(string message, long points)> Upvote(int postId, string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            (string message, long points) = _postService.UpvotePost(user, postId);

            return (message, points);
        }

        public async Task<(string, long)> Downvote(int postId, string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            (string message, long points) = _postService.DownvotePost(user, postId);

            return (message, points);
        }
    }
}