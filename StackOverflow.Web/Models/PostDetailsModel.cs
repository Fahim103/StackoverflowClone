using Autofac;
using StackOverflow.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StackOverflow.Web.Models
{
    public class PostDetailsModel
    {
        private IPostService _postService { get; set; }
        private ICommentService _commentService { get; set; }

        private readonly IPostPointService _postPointService;
        private readonly ICommentPointService _commentPointService;

        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public long Points { get; set; }
        public IList<CommentDetailsModel> CommentDetails { get; set; }

        public PostDetailsModel()
        {
            _postService = Startup.AutofacContainer.Resolve<IPostService>();
            _commentService = Startup.AutofacContainer.Resolve<ICommentService>();
            _postPointService = Startup.AutofacContainer.Resolve<IPostPointService>();
            _commentPointService = Startup.AutofacContainer.Resolve<ICommentPointService>();
        }

        public PostDetailsModel(IPostService postService, ICommentService commentService)
        {
            _postService = postService;
            _commentService = commentService;
        }


        public void GetModelById(int id)
        {
            _postService = new PostService();

            var post = _postService.GetById(id);
            Id = post.Id;
            Title = post.Title;
            Content = post.Content;
            CreatedAt = post.CreatedAt.ToLocalTime();
            Points = _postPointService.GetVotes(post.Id).overall;
            CommentDetails = new List<CommentDetailsModel>();

            foreach (var item in post.Comments)
            {
                CommentDetails.Add(new CommentDetailsModel
                {
                    Id = item.Id,
                    Content = item.Content,
                    IsAccepted = item.IsAccepted,
                    CreatedAt = item.CreatedAt,
                    AnsweredBy = item.ApplicationUser.UserName,
                    Points = _commentPointService.GetCount(item.Id)
                });
            }
        }
    }
}