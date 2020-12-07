using StackOverflow.Core.Entities;
using StackOverflow.Core.Repositories;
using StackOverflow.Core.UnitOfWorks;
using System.Collections.Generic;
using System.Linq;

namespace StackOverflow.Core.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(Comment comment)
        {
            _unitOfWork.BeginTransaction();
            _unitOfWork.CommentRepository.Create(comment);
            _unitOfWork.Commit();
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
