using StackOverflow.Core.Entities;
using StackOverflow.Core.Repositories;
using System.Collections.Generic;

namespace StackOverflow.Core.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public void Create(Post post)
        {
            _postRepository.Create(post);
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public IList<Post> GetAll()
        {
            return _postRepository.Get();
        }

        public Post GetById(int id)
        {
            return _postRepository.Get(id);
        }

        public void Update(Post post)
        {
            _postRepository.Update(post);
        }
    }
}
