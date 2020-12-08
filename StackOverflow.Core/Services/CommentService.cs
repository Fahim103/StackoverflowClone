using NHibernate;
using StackOverflow.Core.Entities;
using StackOverflow.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StackOverflow.Core.Services
{
    public class CommentService : ICommentService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISession _session;

        public CommentService(IUnitOfWork unitOfWork, ISession session)
        {
            _unitOfWork = unitOfWork;
            _session = session;
        }
        public void Create(Comment comment)
        {
            _session.Clear();
            _unitOfWork.BeginTransaction(_session);
            _unitOfWork.CommentRepository.Create(_session, comment);
            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public IList<Comment> GetAll()
        {
            // TODO Parse to object to get lazy laoding
            _session.Clear();
            return _unitOfWork.CommentRepository.Get(_session);
        }

        public Comment Get(int id)
        {
            _session.Clear();
            return _unitOfWork.CommentRepository.Get(_session, id);
        }

        public void Update(Comment comment)
        {
            _session.Clear();
            _unitOfWork.BeginTransaction(_session);
            _unitOfWork.CommentRepository.Update(_session, comment);
            _unitOfWork.Commit();
        }

        public void Dispose()
        {
            if (_session != null)
            {
                _session.Dispose();
            }
        }

        public (string message, long points) UpvoteComment(ApplicationUser user, int commentId)
        {
            _session.Clear();
            var comment = _unitOfWork.CommentRepository.Get(_session, commentId);
            var commentPoint = _unitOfWork.CommentPointRepository.Get(_session, x => x.Comment.Id == commentId && x.ApplicationUser.Id == user.Id).FirstOrDefault();
            if (commentPoint != null)
            {
                if (!commentPoint.IsUpvoted)
                {
                    try
                    {
                        _unitOfWork.BeginTransaction(_session);
                        commentPoint.IsUpvoted = true;
                        _unitOfWork.CommentPointRepository.Update(_session, commentPoint);
                        _unitOfWork.Commit();
                        _session.Clear();
                        return (StringConstants.SUCCESS, _unitOfWork.CommentPointRepository.GetVotes(_session, commentId).overall);
                    }
                    catch (Exception ex)
                    {
                        return (ex.Message, _unitOfWork.CommentPointRepository.GetVotes(_session, commentId).overall);
                    }

                }
                return (StringConstants.DUPLICATE, _unitOfWork.CommentPointRepository.GetVotes(_session, commentId).overall);
            }
            else
            {
                commentPoint = new CommentPoint
                {
                    ApplicationUser = user,
                    IsUpvoted = true,
                    Comment = comment
                };

                _unitOfWork.BeginTransaction(_session);
                _unitOfWork.CommentPointRepository.Create(_session, commentPoint);
                _unitOfWork.Commit();
                _session.Clear();
                return (StringConstants.SUCCESS, _unitOfWork.CommentPointRepository.GetVotes(_session, commentId).overall);
            }
        }

        public (string message, long points) DownvoteComment(ApplicationUser user, int commentId)
        {
            _session.Clear();

            var comment = _unitOfWork.CommentRepository.Get(_session, commentId);
            var commentPoint = _unitOfWork.CommentPointRepository.Get(_session, x => x.Comment.Id == commentId && x.ApplicationUser.Id == user.Id).FirstOrDefault();
            if (commentPoint != null)
            {
                if (commentPoint.IsUpvoted)
                {
                    try
                    {
                        _unitOfWork.BeginTransaction(_session);
                        commentPoint.IsUpvoted = false;
                        _unitOfWork.CommentPointRepository.Update(_session, commentPoint);
                        _unitOfWork.Commit();
                        _session.Clear();

                        return (StringConstants.SUCCESS, _unitOfWork.CommentPointRepository.GetVotes(_session, commentId).overall);
                    }
                    catch (Exception ex)
                    {
                        return (ex.Message, _unitOfWork.CommentPointRepository.GetVotes(_session, commentId).overall);
                    }

                }
                return (StringConstants.DUPLICATE, _unitOfWork.CommentPointRepository.GetVotes(_session, commentId).overall);
            }
            else
            {
                commentPoint = new CommentPoint
                {
                    ApplicationUser = user,
                    IsUpvoted = false,
                    Comment = comment
                };

                _unitOfWork.BeginTransaction(_session);
                _unitOfWork.CommentPointRepository.Create(_session, commentPoint);
                _unitOfWork.Commit();
                _session.Clear();
                return ("Success", _unitOfWork.CommentPointRepository.GetVotes(_session, commentId).overall);
            }
        }
    }
}
