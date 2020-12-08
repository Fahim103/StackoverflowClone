using StackOverflow.Core.Entities;
using StackOverflow.Core.Repositories;
using StackOverflow.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Core.Services
{
    public class CommentPointService : ICommentPointService
    {
        private readonly ICommentPointRepository _commentPointRepository;

        public CommentPointService(ICommentPointRepository commentPointRepository)
        {
            _commentPointRepository = commentPointRepository;
        }

        public void Create(CommentPoint commentPoint)
        {
            _commentPointRepository.Create(commentPoint);
        }

        public CommentPoint GetByUserId(string userId)
        {
            return _commentPointRepository.GetByUserId(userId);
        }

        public int GetCount(int commentId)
        {
            var count = _commentPointRepository.GetCount(x => x.Comment.Id == commentId);
            return count;
        }

        public (long upvote, long downvote, long overall) GetVotes(int commentId)
        {
            return _commentPointRepository.GetVotes(commentId);
        }

        public void Update(CommentPoint commentPoint)
        {
            _commentPointRepository.Update(commentPoint);
        }
    }
}
