using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Autofac;
using StackOverflow.Core.Services;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using StackOverflow.Core.Entities;

namespace StackOverflow.Web.Models
{
    public class CreatePostModel
    {
        private readonly IPostService _postService;
        private readonly ApplicationUserManager _userManager;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Problem title is required")]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Problem description is required")]
        [StringLength(1000, ErrorMessage = "Description length must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        public CreatePostModel()
        {
            _postService = Startup.AutofacContainer.Resolve<IPostService>();
            _userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
        }

        public CreatePostModel(IPostService postService, ApplicationUserManager userManager)
        {
            _postService = postService;
            _userManager = userManager;
        }

        public async Task AddPost(string username)
        {
            var user = await _userManager.FindByEmailAsync(username);
            var post = new Post()
            {
                Title = Title,
                Content = Content,
                CreatedAt = DateTime.UtcNow,
                ApplicationUser = user
            };

            _postService.Create(post);
        }
    }
}