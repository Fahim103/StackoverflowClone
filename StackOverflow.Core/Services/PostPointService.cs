using StackOverflow.Core.Entities;
using StackOverflow.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Core.Services
{
    public class PostPointService : IPostPointService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PostPointService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(PostPoint postPoint)
        {
            throw new NotImplementedException();
        }

        public int GetCount(int postId)
        {
            throw new NotImplementedException();
        }

        public void Update(PostPoint postPoint)
        {
            throw new NotImplementedException();
        }
    }
}
