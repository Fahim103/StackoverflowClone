using StackOverflow.Core.DTO;
using StackOverflow.Core.Entities;
using System.Collections.Generic;

namespace StackOverflow.Core.Services
{
    public interface ICommentService
    {
        IList<Comment> GetAll();
        Comment Get(int id);
        void Create(Comment comment);
        void Update(Comment comment);
        void Delete(int id);
        (string message, long points) UpvoteComment(ApplicationUser user, int commentId);
        (string message, long points) DownvoteComment(ApplicationUser user, int commentId);        
    }
}
