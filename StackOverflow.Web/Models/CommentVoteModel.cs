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

        public async Task<(string, long)> Upvote(int commentId, string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            var comment = _commentService.GetById(commentId);
            var commentPoint = _commentPointService.GetByCommentAndUserId(comment.Id, user.Id);
            if (commentPoint != null)
            {
                if (!commentPoint.IsUpvoted)
                {
                    try
                    {
                        commentPoint.IsUpvoted = true;
                        _commentPointService.Update(commentPoint);
                        return ("Success", _commentPointService.GetVotes(commentId).overall);
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }

                }
                return ("Error", _commentPointService.GetVotes(commentId).overall);
            }
            else
            {
                commentPoint = new CommentPoint
                {
                    ApplicationUser = user,
                    IsUpvoted = true,
                    Comment = comment
                };

                _commentPointService.Create(commentPoint);
                return ("Success", _commentPointService.GetVotes(commentId).overall);
            }
        }

        public async Task<(string, long)> Downvote(int commentId, string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            var comment = _commentService.GetById(commentId);
            var commentPoint = _commentPointService.GetByCommentAndUserId(comment.Id, user.Id);
            if (commentPoint != null)
            {
                if (commentPoint.IsUpvoted)
                {
                    try
                    {
                        commentPoint.IsUpvoted = false;
                        _commentPointService.Update(commentPoint);
                        return ("Success", _commentPointService.GetVotes(commentId).overall);
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }

                }
                return ("Error", _commentPointService.GetVotes(commentId).overall);
            }
            else
            {
                commentPoint = new CommentPoint
                {
                    ApplicationUser = user,
                    IsUpvoted = false,
                    Comment = comment
                };

                _commentPointService.Create(commentPoint);
                return ("Success", _commentPointService.GetVotes(commentId).overall);
            }
        }
    }
}