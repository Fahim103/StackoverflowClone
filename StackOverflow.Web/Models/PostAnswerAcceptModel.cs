using Autofac;
using StackOverflow.Core.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StackOverflow.Web.Models
{
    public class PostAnswerAcceptModel
    {
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;

        [Required]
        public int CommentId { get; set; }
        [Required]
        public int PostId { get; set; }

        public PostAnswerAcceptModel()
        {
            _postService = Startup.AutofacContainer.Resolve<IPostService>();
            _commentService = Startup.AutofacContainer.Resolve<ICommentService>();
        }

        public void AcceptAnswer()
        {
            _postService.AcceptAnswer(PostId, CommentId);
        }
    }
}