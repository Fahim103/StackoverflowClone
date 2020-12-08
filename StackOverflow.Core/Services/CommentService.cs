using StackOverflow.Core.Entities;
using StackOverflow.Core.Repositories;
using StackOverflow.Core.UnitOfWorks;
using System.Collections.Generic;
using System.Linq;

namespace StackOverflow.Core.Services
{
    public class CommentService : ICommentService
    {
        //private readonly IUnitOfWork _unitOfWork;
        private readonly ICommentRepository _commentRepository;

        //public CommentService(IUnitOfWork unitOfWork)
        //{
        //    _unitOfWork = unitOfWork;
        //}

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }
        public void Create(Comment comment)
        {
            _commentRepository.Create(comment);
            //_unitOfWork.BeginTransaction();
            //_unitOfWork.CommentRepository.Create(comment);
            //_unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public IList<Comment> GetAll()
        {
            return _commentRepository.Get();
        }

        public Comment GetById(int id)
        {
            return _commentRepository.Get(id);
        }

        public void Update(Comment comment)
        {
            throw new System.NotImplementedException();
        }
    }
}
