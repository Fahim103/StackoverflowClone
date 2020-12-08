using StackOverflow.Core.Entities;
using StackOverflow.Core.Repositories;
using StackOverflow.Core.UnitOfWorks;
using System.Collections.Generic;
using System.Linq;

namespace StackOverflow.Core.Services
{
    public class PostService : IPostService
    {
        //private readonly IUnitOfWork _unitOfWork;
        private readonly IPostRepository _postRepository;

        //public PostService(IUnitOfWork unitOfWork)
        //{
        //    _unitOfWork = unitOfWork;
        //}

        public PostService()
        {
            _postRepository = new PostRepository();
        }

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public void Create(Post post)
        {
            _postRepository.Create(post);
            //_unitOfWork.BeginTransaction();
            //_unitOfWork.PostRepository.Create(post);
            //_unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public IList<Post> GetAll()
        {
            return _postRepository.Get();
            //return _unitOfWork.PostRepository.Get().ToList();
        }

        public Post GetById(int id)
        {
            return _postRepository.Get(id);
            //return _unitOfWork.PostRepository.Get(id);
        }

        public void Update(Post post)
        {
            throw new System.NotImplementedException();
        }
    }
}
