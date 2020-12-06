using Autofac;
using StackOverflow.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StackOverflow.Web.Models
{
    public class PostModel
    {
        private readonly IPostService _postService;

        public virtual string Title { get; set; }
        public virtual string Content { get; set; }
        public virtual DateTime CreatedAt { get; set; }

        public IList<PostModel> Posts { get; private set; }


        public PostModel()
        {
            _postService = Startup.AutofacContainer.Resolve<IPostService>();
        }

        public PostModel(IPostService postService)
        {
            _postService = postService;
        }

        public void LoadModelData()
        {
            var posts = _postService.GetAll();
            Posts = new List<PostModel>();
            foreach(var post in posts)
            {
                Posts.Add(new PostModel()
                {
                    Title = post.Title,
                    Content = post.Content,
                    CreatedAt = post.CreatedAt.ToLocalTime()
                });
            }
        }
    }
}