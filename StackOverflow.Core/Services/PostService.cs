using StackOverflow.Core.Entities;
using StackOverflow.Core.Repositories;
using StackOverflow.Core.UnitOfWorks;
using System.Collections.Generic;
using System.Linq;

namespace StackOverflow.Core.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PostService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(Post post)
        {
            _unitOfWork.BeginTransaction();
            _unitOfWork.PostRepository.Create(post);
            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public IList<Post> GetAll()
        {
            return _unitOfWork.PostRepository.Get().ToList();
        }

        public Post GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Post post)
        {
            throw new System.NotImplementedException();
        }
    }
}
