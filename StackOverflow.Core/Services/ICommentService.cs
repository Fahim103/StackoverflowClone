using StackOverflow.Core.Entities;
using System.Collections.Generic;

namespace StackOverflow.Core.Services
{
    public interface ICommentService
    {
        IList<Comment> GetAll();
        Comment GetById(int id);
        void Create(Comment comment);
        void Update(Comment comment);
        void Delete(int id);
    }
}
