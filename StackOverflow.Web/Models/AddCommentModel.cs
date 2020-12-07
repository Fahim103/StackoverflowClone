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
    public class AddCommentModel
    {
        private readonly ICommentService _commentService;
        private readonly IPostService _postService;
        private readonly ApplicationUserManager _userManager;

        public int PostId { get; set; }
        public string Content { get; set; }

        public AddCommentModel()
        {
            _commentService = Startup.AutofacContainer.Resolve<ICommentService>();
            _postService = Startup.AutofacContainer.Resolve<IPostService>();
            _userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
        }

        public async Task AddComment(string username)
        {
            var user = await _userManager.FindByEmailAsync(username);
            var post = _postService.GetById(PostId);
            var comment = new Comment()
            {
                Content = Content,
                CreatedAt = DateTime.UtcNow,
                ApplicationUser = user,
                Post = post
            };

            _commentService.Create(comment);
        }
    }
}