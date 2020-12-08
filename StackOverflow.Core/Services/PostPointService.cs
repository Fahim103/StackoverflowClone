using StackOverflow.Core.Entities;
using StackOverflow.Core.Repositories;
using StackOverflow.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Core.Services
{
    public class PostPointService : IPostPointService
    {
        private readonly IPostPointRepository _postPointRepository;

        public PostPointService(IPostPointRepository postPointRepository)
        {
            _postPointRepository = postPointRepository;
        }


        public void Create(PostPoint postPoint)
        {
            _postPointRepository.Create(postPoint);
        }

        public PostPoint GetByUserId(string userId)
        {
            return _postPointRepository.GetByUserId(userId);
        }

        public int GetCount(Expression<Func<PostPoint, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public (long upvote, long downvote, long overall) GetVotes(int postId)
        {
            return _postPointRepository.GetVotes(postId);
        }

        public void Update(PostPoint postPoint)
        {
            _postPointRepository.Update(postPoint);
        }
    }
}
