using StackOverflow.Core.Entities;
using System.Collections.Generic;

namespace StackOverflow.Core.Services
{
    public interface IPostService
    {
        IList<Post> GetAll();
        Post GetById(int id);
        void Create(Post post);
        void Update(Post post);
        void Delete(int id);
    }
}
