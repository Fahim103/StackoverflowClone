using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Core.Entities
{
    public class Comment
    {
        public virtual int Id { get; set; }
        public virtual string Content { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual string UserId { get; set; }
        public virtual bool IsAccepted { get; set; }
        public virtual ICollection<CommentPoint> CommentPoints { get; set; }
    }
}
