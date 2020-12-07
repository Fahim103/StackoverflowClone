using StackOverflow.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Core.Services
{
    public interface IPostPointService
    {
        void Update(PostPoint postPoint);
        void Create(PostPoint postPoint);
        int GetCount(int postId);
    }
}
