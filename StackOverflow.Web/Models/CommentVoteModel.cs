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
    public class CommentVoteModel
    {
        private readonly ICommentService _commentService;
        private readonly ICommentPointService _commentPointService;
        private readonly ApplicationUserManager _userManager;

        public CommentVoteModel()
        {
            _commentPointService = Startup.AutofacContainer.Resolve<ICommentPointService>();
            _commentService = Startup.AutofacContainer.Resolve<ICommentService>();
            _userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
        }

        public CommentVoteModel(ICommentService commentService, ICommentPointService commentPointService)
        {
            _commentService = commentService;
            _commentPointService = commentPointService;
            _userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
        }

        public async Task<(string message, long points)> Upvote(int commentId, string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            (string message, long points) = _commentService.UpvoteComment(user, commentId);

            return (message, points);
        }

        public async Task<(string, long)> Downvote(int commentId, string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            (string message, long points) = _commentService.DownvoteComment(user, commentId);

            return (message, points);
        }
    }
}