using NHibernate;
using StackOverflow.Core.DTO;
using StackOverflow.Core.Entities;
using StackOverflow.Core.Exceptions;
using StackOverflow.Core.Repositories;
using StackOverflow.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StackOverflow.Core.Services
{
    public class PostService : IPostService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISession _session;

        public PostService(IUnitOfWork unitOfWork, ISession session)
        {
            _unitOfWork = unitOfWork;
            _session = session;
        }

        public void Create(Post post)
        {
            _session.Clear();
            _unitOfWork.BeginTransaction(_session);
            _unitOfWork.PostRepository.Create(_session, post);
            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            if(_session != null)
            {
                _session.Dispose();
            }
        }

        public IList<PostModelDTO> GetAll()
        {
            _session.Clear();
            var posts = _unitOfWork.PostRepository.Get(_session, x => x.IsDeleted == false);
            var result = new List<PostModelDTO>();
            foreach(var post in posts)
            {
                result.Add(new PostModelDTO()
                {
                    Id = post.Id,
                    Title = post.Title,
                    Content = post.Content,
                    CreatedAt = post.CreatedAt.ToLocalTime(),
                    IsDuplicate = post.IsDuplicate,
                    TotalVotes = _unitOfWork.PostPointRepository.GetVotes(_session, post.Id).overall,
                    TotalAnswers = _unitOfWork.CommentRepository.GetCount(_session, x => x.Post.Id == post.Id)
                });
            }

            return result;
        }

        public Post Get(int id)
        {
            _session.Clear();
            return _unitOfWork.PostRepository.Get(_session, x => x.Id == id && x.IsDeleted == false).FirstOrDefault();
        }

        public PostModelDTO GetById(int id)
        {
            //_session.Clear();
            //var post = _unitOfWork.PostRepository.Get(_session, id);

            var post = Get(id);


            if (post == null)
            {
                throw new EntityNotFoundException("No post found", nameof(Post));
            }
            var postModelDTO = new PostModelDTO()
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                AskedBy = post.ApplicationUser.UserName,
                CreatedAt = post.CreatedAt,
                TotalVotes = _unitOfWork.PostPointRepository.GetVotes(_session, post.Id).overall,
                TotalAnswers = _unitOfWork.CommentRepository.GetCount(_session, x => x.Post.Id == post.Id),
                HasAcceptedAnswer = post.HasAcceptedAnswer,
                Comments = new List<CommentModelDTO>()
            };

            foreach(var comment in post.Comments)
            {
                postModelDTO.Comments.Add(new CommentModelDTO()
                {
                    Id = comment.Id,
                    Content = comment.Content,
                    CreatedAt = comment.CreatedAt,
                    IsAccepted = comment.IsAccepted,
                    AnsweredBy = comment.ApplicationUser.UserName,
                    CommentPoints = _unitOfWork.CommentPointRepository.GetVotes(_session, comment.Id).overall
                });
            }

            return postModelDTO;
        }

        public void Update(Post post)
        {
            _session.Clear();
            _unitOfWork.BeginTransaction(_session);
            _unitOfWork.PostRepository.Update(_session, post);
            _unitOfWork.Commit();
        }

        public (string message, long points) UpvotePost(ApplicationUser user, int postId)
        {
            _session.Clear();
            var post = _unitOfWork.PostRepository.Get(_session, postId);
            var postPoint = _unitOfWork.PostPointRepository.Get(_session, x => x.Post.Id == postId && x.ApplicationUser.Id == user.Id).FirstOrDefault();
            if (postPoint != null)
            {
                if (!postPoint.IsUpvoted)
                {
                    try
                    {
                        _unitOfWork.BeginTransaction(_session);
                        postPoint.IsUpvoted = true;
                        _unitOfWork.PostPointRepository.Update(_session, postPoint);
                        _unitOfWork.Commit();
                        _session.Clear();
                        
                        return (StringConstants.SUCCESS, _unitOfWork.PostPointRepository.GetVotes(_session, postId).overall);
                    }
                    catch (Exception ex)
                    {
                        return (ex.Message, _unitOfWork.PostPointRepository.GetVotes(_session, postId).overall);
                    }
                }

                return (StringConstants.DUPLICATE, _unitOfWork.PostPointRepository.GetVotes(_session, postId).overall);
            }
            else
            {
                postPoint = new PostPoint
                {
                    ApplicationUser = user,
                    IsUpvoted = true,
                    Post = post
                };

                _unitOfWork.BeginTransaction(_session);
                _unitOfWork.PostPointRepository.Create(_session, postPoint);
                _unitOfWork.Commit();
                _session.Clear();

                return (StringConstants.SUCCESS, _unitOfWork.PostPointRepository.GetVotes(_session, postId).overall);
            }
        }

        public (string message, long points) DownvotePost(ApplicationUser user, int postId)
        {
            _session.Clear();
            var post = _unitOfWork.PostRepository.Get(_session, postId);
            var postPoint = _unitOfWork.PostPointRepository.Get(_session, x => x.Post.Id == postId && x.ApplicationUser.Id == user.Id).FirstOrDefault();
            if (postPoint != null)
            {
                if (postPoint.IsUpvoted)
                {
                    try
                    {
                        _unitOfWork.BeginTransaction(_session);
                        postPoint.IsUpvoted = false;
                        _unitOfWork.PostPointRepository.Update(_session, postPoint);
                        _unitOfWork.Commit();
                        _session.Clear();

                        return (StringConstants.SUCCESS, _unitOfWork.PostPointRepository.GetVotes(_session, postId).overall);
                    }
                    catch (Exception ex)
                    {
                        return (ex.Message, _unitOfWork.PostPointRepository.GetVotes(_session, postId).overall);
                    }

                }
                
                return (StringConstants.DUPLICATE, _unitOfWork.PostPointRepository.GetVotes(_session, postId).overall);
            }
            else
            {
                postPoint = new PostPoint
                {
                    ApplicationUser = user,
                    IsUpvoted = false,
                    Post = post
                };

                _unitOfWork.BeginTransaction(_session);
                _unitOfWork.PostPointRepository.Create(_session, postPoint);
                _unitOfWork.Commit();
                _session.Clear();

                return (StringConstants.SUCCESS, _unitOfWork.PostPointRepository.GetVotes(_session, postId).overall);
            }
        }

        public void AcceptAnswer(int postId, int commentId)
        {
            _session.Clear();
         
            var post = Get(postId);
            var comment = _unitOfWork.CommentRepository.Get(_session, commentId);
            
            _unitOfWork.BeginTransaction(_session);
            
            post.HasAcceptedAnswer = true;
            comment.IsAccepted = true;
            
            _unitOfWork.PostRepository.Update(_session, post);
            _unitOfWork.CommentRepository.Update(_session, comment);

            _unitOfWork.Commit();
            _session.Clear();
        }

        public void MarkDuplicate(int postId)
        {
            if (postId == 0)
                return;

            _session.Clear();

            var post = Get(postId);
            if (post.IsDuplicate)
            {
                _session.Clear();
                return;
            }

            _unitOfWork.BeginTransaction(_session);
            post.IsDuplicate = true;
            _unitOfWork.PostRepository.Update(_session, post);
            _unitOfWork.Commit();
        }

        public void HidePost(int postId)
        {
            if (postId == 0)
                return;

            _session.Clear();

            var post = Get(postId);
            if (post.IsDeleted)
            {
                _session.Clear();
                return;
            }

            _unitOfWork.BeginTransaction(_session);
            post.IsDeleted = true;
            _unitOfWork.PostRepository.Update(_session, post);
            _unitOfWork.Commit();
        }
    }
}
