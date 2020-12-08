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

        public async Task<(string, long)> Upvote(int postId, string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            var post = _postService.GetById(postId);
            var postPoint = _postPointService.GetByPostAndUserId(post.Id, user.Id);
            if (postPoint != null)
            {
                if (!postPoint.IsUpvoted)
                {
                    try
                    {
                        postPoint.IsUpvoted = true;
                        _postPointService.Update(postPoint);
                        return ("Success", _postPointService.GetVotes(postId).overall);
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }
                    
                }
                return ("Error", _postPointService.GetVotes(postId).overall);
            }
            else
            {
                postPoint = new PostPoint
                {
                    ApplicationUser = user,
                    IsUpvoted = true,
                    Post = post
                };

                _postPointService.Create(postPoint);
                return ("Success", _postPointService.GetVotes(postId).overall);
            }
        }

        public async Task<(string, long)> Downvote(int postId, string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            var post = _postService.GetById(postId);
            var postPoint = _postPointService.GetByPostAndUserId(post.Id, user.Id);
            if (postPoint != null)
            {
                if (postPoint.IsUpvoted)
                {
                    try
                    {
                        postPoint.IsUpvoted = false;
                        _postPointService.Update(postPoint);
                        return ("Success", _postPointService.GetVotes(postId).overall);
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }

                }
                return ("Error", _postPointService.GetVotes(postId).overall);
            }
            else
            {
                postPoint = new PostPoint
                {
                    ApplicationUser = user,
                    IsUpvoted = false,
                    Post = post
                };

                _postPointService.Create(postPoint);
                return ("Success", _postPointService.GetVotes(postId).overall);
            }
        }
    }
}