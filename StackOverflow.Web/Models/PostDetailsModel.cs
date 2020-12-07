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

        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Points { get; set; }
        public IList<CommentDetailsModel> CommentDetails { get; set; }

        public PostDetailsModel()
        {
            _postService = Startup.AutofacContainer.Resolve<IPostService>();
        }

        public PostDetailsModel(IPostService postService)
        {
            _postService = postService;
        }


        public void GetModelById(int id)
        {
            var post = _postService.GetById(id);
            Id = post.Id;
            Title = post.Title;
            Content = post.Content;
            CreatedAt = post.CreatedAt.ToLocalTime();
            CommentDetails = new List<CommentDetailsModel>();

            foreach (var item in post.Comments)
            {
                // TODO Calculate points
                CommentDetails.Add(new CommentDetailsModel
                {
                    Id = item.Id,
                    Content = item.Content,
                    IsAccepted = item.IsAccepted,
                    CreatedAt = item.CreatedAt,
                    AnsweredBy = item.ApplicationUser.UserName,
                    Points = 0
                });
            }
        }
    }
}