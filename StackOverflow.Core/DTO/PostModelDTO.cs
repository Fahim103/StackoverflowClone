using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Core.DTO
{
    public class PostModelDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string AskedBy { get; set; }
        public long TotalVotes { get; set; }
        public long TotalAnswers { get; set; }
        public bool HasAcceptedAnswer { get; set; }

        public IList<CommentModelDTO> Comments { get; set; }

    }
}
