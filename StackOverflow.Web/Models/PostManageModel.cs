using Autofac;
using StackOverflow.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StackOverflow.Web.Models
{
    public class PostManageModel
    {
        private readonly IPostService _postService;

        public PostManageModel()
        {
            _postService = Startup.AutofacContainer.Resolve<IPostService>();
        }


        public void MarkPostDuplicate(int postId)
        {
            if (postId == 0)
                return;

            _postService.MarkDuplicate(postId);
        }

        public void HidePost(int postId)
        {
            if (postId == 0)
                return;

            _postService.HidePost(postId);
        }
    }
}