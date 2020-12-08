using NHibernate;
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
    public class PostPointService : IPostPointService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISession _session;

        public PostPointService(IUnitOfWork unitOfWork, ISession session)
        {
            _unitOfWork = unitOfWork;
            _session = session;
        }


        public void Create(PostPoint postPoint)
        {
            _session.Clear();
            _unitOfWork.BeginTransaction(_session);
            _unitOfWork.PostPointRepository.Create(_session, postPoint);
            _unitOfWork.Commit();
        }

        public PostPoint GetByPostAndUserId(int postId, string userId)
        {
            _session.Clear();
            return _unitOfWork.PostPointRepository.Get(_session, x => x.Post.Id == postId && x.ApplicationUser.Id == userId).FirstOrDefault();
        }

        public int GetCount(Expression<Func<PostPoint, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public (long upvote, long downvote, long overall) GetVotes(int postId)
        {
            _session.Clear();
            return _unitOfWork.PostPointRepository.GetVotes(_session, postId);
        }

        public void Update(PostPoint postPoint)
        {
            _session.Clear();
            _unitOfWork.BeginTransaction(_session);
            _unitOfWork.PostPointRepository.Update(_session, postPoint);
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
