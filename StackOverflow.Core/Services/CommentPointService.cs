using NHibernate;
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
    public class CommentPointService : ICommentPointService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISession _session;

        public CommentPointService(IUnitOfWork unitOfWork, ISession session)
        {
            _unitOfWork = unitOfWork;
            _session = session;
        }

        public void Create(CommentPoint commentPoint)
        {
            _session.Clear();
            _unitOfWork.BeginTransaction(_session);
            _unitOfWork.CommentPointRepository.Create(_session, commentPoint);
            _unitOfWork.Commit();
        }

        public CommentPoint GetByCommentAndUserId(int commentId,string userId)
        {
            _session.Clear();
            return _unitOfWork.CommentPointRepository.Get(_session, x => x.Comment.Id == commentId && x.ApplicationUser.Id == userId).FirstOrDefault();
        }

        public int GetCount(int commentId)
        {
            _session.Clear();
            var count = _unitOfWork.CommentPointRepository.GetCount(_session, x => x.Comment.Id == commentId);
            return count;
        }

        public (long upvote, long downvote, long overall) GetVotes(int commentId)
        {
            _session.Clear();
            return _unitOfWork.CommentPointRepository.GetVotes(_session, commentId);
        }

        public void Update(CommentPoint commentPoint)
        {
            _session.Clear();
            _unitOfWork.BeginTransaction(_session);
            _unitOfWork.CommentPointRepository.Update(_session, commentPoint);
            _unitOfWork.Commit();
        }

        public void Dispose()
        {
            if (_session != null)
            {
                _session.Dispose();
            }
        }
    }
}
