using StackOverflow.Core.Entities;
using StackOverflow.Core.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace StackOverflow.Core.Services
{
    public class CommentService : ICommentService
    {
        private IRepository<Comment> _commentRepository;

        public CommentService(IRepository<Comment> commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public void Create(Comment comment)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public IList<Comment> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Comment GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Comment comment)
        {
            throw new System.NotImplementedException();
        }
    }
}
