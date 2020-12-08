using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Core.DTO
{
    public class CommentModelDTO
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string AnsweredBy { get; set; }
        public bool IsAccepted { get; set; }
        public long CommentPoints { get; set; }
        public PostModelDTO Post { get; set; }
    }
}
