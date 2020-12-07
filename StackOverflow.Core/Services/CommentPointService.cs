using StackOverflow.Core.Entities;
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
        private readonly IUnitOfWork _unitOfWork;

        public CommentPointService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(CommentPoint commentPoint)
        {
            throw new NotImplementedException();
        }

        public int GetCount(int commentId)
        {
            throw new NotImplementedException();
        }

        public void Update(CommentPoint commentPoint)
        {
            throw new NotImplementedException();
        }
    }
}
