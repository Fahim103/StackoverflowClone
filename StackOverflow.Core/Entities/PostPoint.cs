using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Core.Entities
{
    public class PostPoint
    {
        public virtual int Id { get; set; }
        public virtual bool IsUpvoted { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Post Post { get; set; }
    }
}
